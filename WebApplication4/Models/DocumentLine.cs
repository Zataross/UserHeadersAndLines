using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication4.Models
{
    public class DocumentLine
    {
        public int DocumentLineId { get; set; }
        [Required] public string Content { get; set; }
        public int DocumentHeaderId { get; set; }
        [JsonIgnore] public DocumentHeader DocumentHeader { get; set; }
    }
}
