using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication4.Models
{
    public class DocumentHeader
    {
        public int DocumentHeaderId { get; set; }
        [Required] public string Title { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]public User User { get; set; }
        [Required] public ICollection<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    }
}
