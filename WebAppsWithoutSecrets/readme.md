## Web Application Without Secrets

<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fsanjaybhagia%2Fwebappsecretsdemo%2Fmaster%2FWebAppsWithoutSecrets%2Fdeployment%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/> 
</a>

## Description 
ASP.Net Core Web application (**WebAppsWithoutSecrets**) which lists blobs (URIs only) from a container (demoblobs) in the Storage Account. In this sample, web app doesn't have any connection string or key to talk to Storage Account. However, Azure KeyVault has the ConnectionString to the Storage Account stored as a secret. Web Application fetches this ConnectionString from Azure KeyVault using Managed Service Identity (MSI)

The Container is created when application starts and a dummy blob is created as well. 

The ARM Template does the following: 

* provisions a Storage Account
* provisions the KeyVault
* provisions the web application
* adds 'StorageAccountConnectionString' as a Secret in the KeyVault
* sets access policy in Azure KeyVault for the web application
* sets the name of the KeyVault in application settings




