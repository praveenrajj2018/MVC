using System.Threading.Tasks;
using LoginApp.Models;

namespace LoginApp.Repositories
{
    public interface IAttachmentRepository
    {
        Task SaveImagePath(string imagePath);
        Task<Attachment> GetAttachmentById(int id); // Add this
        Task DeleteImage(int id); // Add this
    }
}
