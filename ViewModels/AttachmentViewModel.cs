using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LoginApp.ViewModels
{
    public class AttachmentViewModel
    {
        public int Id { get; set; } // Add this property

        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
        public string UploadedBy { get; set; } // Existing property for the username
    }
}
