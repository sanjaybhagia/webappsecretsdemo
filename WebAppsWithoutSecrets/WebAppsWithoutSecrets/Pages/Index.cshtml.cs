using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebAppsWithoutSecrets.Pages
{
    public class IndexModel : PageModel
    {
        private static string blobStorageConnectionString;

        //decorate this property with BindProperty so we can access it on the page
        [BindProperty]
        public List<string> BlobUris { get; set; }

        //Create a constructor to inject IConfiguration object from DI
        public IndexModel(IConfiguration configurations)
        {
            //Instead of reading connection strings from app settings (configurations.GetConnectionString("StorageAccountConnectionString"))
            //We read it directly from KeyVault as a secret
            blobStorageConnectionString = configurations["StorageAccountConnectionString"];
        }

        //make this async
        public async Task OnGet()
        {
            BlobUris = await ListBlobsAsync();
        }

        private static async Task<List<string>> ListBlobsAsync()
        {
            List<string> blobUris = new List<string>();
            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudStorageAccount storageAccount;
            CloudStorageAccount.TryParse(blobStorageConnectionString, out storageAccount);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("demoblobs");
            await cloudBlobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, null, null);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("testblob");
            await cloudBlockBlob.UploadTextAsync("test file uploaded for demo");

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    Console.WriteLine(item.Uri.ToString());
                    blobUris.Add(item.Uri.ToString());
                }
            } while (blobContinuationToken != null);

            return blobUris;
        }
    }
}
