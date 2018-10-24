## Web Application Without Secrets

<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2F101-DDoS-Attack-Prevention%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/> 
</a>

## Description 
ASP.Net Core Web application (**WebAppsWithSecrets**) which lists blobs (URIs only) from a container (demoblobs) in the Storage Account. In this sample, web app has a connection string to the Storage Account. 

The Container is created when application starts and a dummy blob is created as well. 

The ARM Template does the following: 

* provisions a Storage Account
* provisions the web application
* adds 'StorageAccountConnectionString' as a Connection String in the Web Application




