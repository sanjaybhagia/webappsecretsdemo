using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace WebAppsWithoutSecrets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //this is where KeyVault magic happens - we are setting up configurations from Azure KeyVault using Managed Service Identity
            //without specifiying any details of the Azure KeyVault itself (except the Url of the vault)
            .ConfigureAppConfiguration((context, config) =>
            {
                var builtConfig = config.Build();
                var keyVaultUrl = $"https://{builtConfig["KeyVaultName"]}.vault.azure.net";
                //AzureServiceTokenProvider - this is the magic piece that makes it seamless to work with MSI even on your local development machine
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                config.AddAzureKeyVault(keyVaultUrl, keyVaultClient, new DefaultKeyVaultSecretManager());
            })
                .UseStartup<Startup>()
                .Build();
    }
}
