using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoginApp.ViewModels;
using System.IO;
using System.Threading.Tasks;
using LoginApp.Services;

namespace LoginApp.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentsController(IAttachmentService attachmentService, IWebHostEnvironment webHostEnvironment)
        {
            _attachmentService = attachmentService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Attachment()
        {
            var model = new AttachmentViewModel();
            return View("~/Views/Home/Attachment.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(AttachmentViewModel model)
        {
            if (model.ImageFile != null)
            {
                var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                var filePath = Path.Combine(uploadsDir, model.ImageFile.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                model.ImagePath = $"{baseUrl}/uploads/{model.ImageFile.FileName}";

                // Save the image path to the database
                await _attachmentService.SaveImagePath(model.ImagePath);
            }

            return View("~/Views/Home/Attachment.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var attachment = await _attachmentService.GetAttachmentById(id);
            if (attachment != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(attachment.ImagePath));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                await _attachmentService.DeleteImage(id);
            }

            return RedirectToAction("Attachment");
        }
    }
}
