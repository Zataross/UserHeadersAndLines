using WebApplication4.Models;
namespace WebApplication4.Interfaces
{
    public interface IDocumentHeadersRepository
    {
        ICollection<User> UserDocuments();
    }
}
