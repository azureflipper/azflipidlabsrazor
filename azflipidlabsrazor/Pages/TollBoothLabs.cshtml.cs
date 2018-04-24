using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AZFLIP_IDLABS.Pages
{
    public class TollBoothLabsModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Front-End environment for AzFlip Functions";
        }
    }
}