using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace azflipidlabsrazor.Controllers
{
    [Route("api/[controller]")]

    public class FileUploadController : Controller
    {
        [HttpPost("[Action]")]
        async public Task<IActionResult> SaveFile(IFormFile files)
        {
            // Connect to Azure
            // Set the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=azfltollboothblob001;AccountKey=zFEwiuow6NW/gINn+xsxzBDdvRso2Q7+JBSAp55MEafnV1p04oR/fxalt7sg0c5AtEyW5LN5qNVeLpg+obxviw==;EndpointSuffix=core.windows.net");

            // Create a blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container  
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            // Save file to blob
            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(files.FileName);

            // Create or overwrite the blob with the contents of a local file 
            using (var fileStream = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            //return View();
            // Respond with success
            return Json(new
            {
                name = blockBlob.Name,
                uri = blockBlob.Uri,
                size = blockBlob.Properties.Length
            });
        }
    }
}