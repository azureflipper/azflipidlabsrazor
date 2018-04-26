using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace azflipidlabsrazor.Pages
{
    public class TollBoothLabsModel : PageModel
    {
        public string FileName { get; set; }
        public string AzureUrl { get; set; }
        public string Message { get; set; }
        public List<TollBoothLabsModel> FileList { get; set; }

        public void OnGet()
        {
            Message = "Front-End environment for AzFlip Functions";

        }

        public async Task<List<Uri>> GetFullBlobsAsync()
        {
            // Connect to Azure
            // Set the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=azfltollboothblob001;AccountKey=zFEwiuow6NW/gINn+xsxzBDdvRso2Q7+JBSAp55MEafnV1p04oR/fxalt7sg0c5AtEyW5LN5qNVeLpg+obxviw==;EndpointSuffix=core.windows.net");

            // Create a blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container  
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            var blobList = await container.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.None, int.MaxValue, null, null, null);

            var FileList = blobList;

            return (from blob in blobList.Results where !blob.Uri.Segments.LastOrDefault().EndsWith("-thumb") select blob.Uri).ToList();
        }
    }
}