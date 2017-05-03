using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;


namespace throwaway
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       private void button2_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = null;
           try
           {
               if (textBox1.Text.StartsWith("https"))
               {
                   //Do webrequest to get info on secure site
                   ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                   request = (HttpWebRequest) WebRequest.Create(textBox1.Text);
                   HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                   using (var responseStream = response.GetResponseStream())
                   using (var responseReader = new StreamReader(responseStream))
                   {
                       var contents = responseReader.ReadToEnd();
                   }
                   response.Close();
                   //retrieve the ssl cert and assign it to an X509Certificate object
                   X509Certificate cert = request.ServicePoint.Certificate;
                   //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
                   X509Certificate2 cert2 = new X509Certificate2(cert);
                   //Display the Cert and give the user the option of grabbing or just exiting once they see the contents
                   DialogResult dialogResult = MessageBox.Show("Cert Details = " + cert.Subject,
                       "Grab the Cert?", MessageBoxButtons.YesNo);
                   if (dialogResult == DialogResult.Yes)
                   {
                       //display the cert dialog box
                       X509Certificate2UI.DisplayCertificate(cert2);
                   }
                   else if (dialogResult == DialogResult.No)
                   {
                       return;
                   }
               }
                else
                {
                    MessageBox.Show("You must provide an https:// URL for the app to grab the cert");
                }
            }
        
           catch
               (WebException
                   ex)
           {
               //This is needed if the Broken HTTPS page give us a 404 response code
             
               HttpWebResponse webResponse = (HttpWebResponse) ex.Response;
               if (webResponse == null)
               {
                   MessageBox.Show(
                       "We appear to have got a null response from the webserver this usually means we were redirected to a non secure http:// only site");
                    return;
               }

               if (webResponse.StatusCode == HttpStatusCode.NotFound)
               {
                   webResponse.Close();
                   //retrieve the ssl cert and assign it to an X509Certificate object
                   X509Certificate cert = request.ServicePoint.Certificate;
                   //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
                   X509Certificate2 cert2 = new X509Certificate2(cert);
                   //display the cert dialog box
                   X509Certificate2UI.DisplayCertificate(cert2);
               }
               MessageBox.Show("Cert Page gives 404 Error , we should still have been able to grab the Cert");
           }
        }
        }
    }

        
    
