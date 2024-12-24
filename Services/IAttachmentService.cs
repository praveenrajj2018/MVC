using System.Threading.Tasks;
using LoginApp.Models;

namespace LoginApp.Services
{
    public interface IAttachmentService
    {
        Task SaveImagePath(string imagePath);
        Task<Attachment> GetAttachmentById(int id); // Add this
        Task DeleteImage(int id); // Add this
    }
}
