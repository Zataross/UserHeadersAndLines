namespace WebApplication4.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<DocumentHeader> DocumentHeaders { get; set; }
    }
}
