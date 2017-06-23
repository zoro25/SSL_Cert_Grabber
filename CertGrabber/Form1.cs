using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using throwaway.Properties;


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
                   response.Close();
                   //retrieve the ssl cert and assign it to an X509Certificate object
                   var cert = request.ServicePoint.Certificate;
                   //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
                   if (cert != null)
                   {
                       var cert2 = new X509Certificate2(cert);
                       //Display the Cert and give the user the option of grabbing or just exiting once they see the contents
                       var dialogResult = MessageBox.Show(Resources.Cert_Details + cert.Subject,
                           Resources.Grab_the_Cert_, MessageBoxButtons.YesNo);
                       switch (dialogResult)
                       {
                           case DialogResult.Yes:
                               //display the cert dialog box
                               X509Certificate2UI.DisplayCertificate(cert2);
                               break;
                           case DialogResult.No:
                               return;
                       }
                   }
               }
                else
                {
                    var cert = request.ServicePoint.Certificate;
                    if (cert != null)
                    {
                        var cert2 = new X509Certificate2(cert);
                        //Display the Cert and give the user the option of grabbing or just exiting once they see the contents
                        var dialogResult = MessageBox.Show(Resources.Cert_Details + cert.Subject,
                            Resources.Grab_the_Cert_, MessageBoxButtons.YesNo);
                        switch (dialogResult)
                        {
                            case DialogResult.Yes:
                                //display the cert dialog box
                                X509Certificate2UI.DisplayCertificate(cert2);
                                break;
                            case DialogResult.No:
                                return;
                        }
                    }
                    MessageBox.Show(Resources.NonHttpsMessage);
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
                       Resources.HttpstoHttpRedirectMessage);
                   // return;
               }


               if (webResponse.StatusCode == HttpStatusCode.NotFound| webResponse.StatusCode == HttpStatusCode.Forbidden)
               {
                   webResponse.Close();
                   //retrieve the ssl cert and assign it to an X509Certificate object
                   var cert = request?.ServicePoint.Certificate;
                   //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
                   if (cert != null)
                   {
                       var cert2 = new X509Certificate2(cert);
                       //display the cert dialog box
                       X509Certificate2UI.DisplayCertificate(cert2);
                   }
               }
               MessageBox.Show(Resources.Cert404StillGotCertMessage);
           }
            }
        }
        }
    