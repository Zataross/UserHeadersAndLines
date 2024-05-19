using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Repository
{
    public class DocumentHeadersRepository
    {
        private readonly AppDbContext _context;

        public DocumentHeadersRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<User> UserDocuments()
        {
            return _context.Users.OrderBy(u => u.UserId).ToList();
        }
    }
}
