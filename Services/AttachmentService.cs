using LoginApp.Models;
using LoginApp.Repositories;
using System.Threading.Tasks;

namespace LoginApp.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task SaveImagePath(string imagePath)
        {
            await _attachmentRepository.SaveImagePath(imagePath);
        }

        public async Task<Attachment> GetAttachmentById(int id)
        {
            return await _attachmentRepository.GetAttachmentById(id);
        }

        public async Task DeleteImage(int id)
        {
            await _attachmentRepository.DeleteImage(id);
        }
    }
}
