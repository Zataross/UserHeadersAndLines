namespace WebApplication4.Models
{
    public class AddDocumentWithLinesDto
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public List<AddDocumentLineDto> DocumentLines { get; set; }
    }
}
