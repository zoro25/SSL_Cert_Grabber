# Cert-Grabber
Tool to Grab SSL Certs from Sites

# Why
I had a need to import a cert from a site that was incorrectly configured. 
All modern browsers will allow viewing and importing of SSL certs so long as the site is configured correctly. 

# Example
Go to https://support.hbgary.com/ and you'll get the error that the site is insecure as the cert is incorrect. 
This error is correct and most browsers will give the user some basic information about the invalid cert but none will allow you to view it in detail and also none will allow the user to export or save/import that cert to your local machine.

# Other Uses 
There may be others , for example grabbing valid certs from sites for example https://twitter.com/thetestmanager/ , however I'm guessing clicking on the padlock in your browser is an easier option. It's main purpose is grabbing bad certs when browsers are blocking you.

# Other ways to grab an invalid cert 
You could always install Open-SSL and that should allow you to grab an invalid cert. 

# Updates/Bug-Fixes
I doubt any will be released or needed , it is a small Proof of concept piece of code which filled a need. 
