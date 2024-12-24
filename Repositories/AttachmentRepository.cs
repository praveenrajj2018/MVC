using LoginApp.Data;
using LoginApp.Models;
using System.Threading.Tasks;

namespace LoginApp.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveImagePath(string imagePath)
        {
            var attachment = new Attachment { ImagePath = imagePath };
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment> GetAttachmentById(int id)
        {
            return await _context.Attachments.FindAsync(id);
        }

        public async Task DeleteImage(int id)
        {
            var attachment = await GetAttachmentById(id);
            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
