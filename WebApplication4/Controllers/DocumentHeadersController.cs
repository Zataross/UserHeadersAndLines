using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentHeadersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentHeadersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("UserDocuments")]
        public async Task<ActionResult<IEnumerable<DocumentHeaderDto>>> GetDocumentHeaders()
        {
            var username = User.Identity.Name;
            var data = await _context.DocumentHeaders
                .Select(dh => new DocumentHeaderDto
                {
                    DocumentHeaderId = dh.DocumentHeaderId,
                    Title = dh.Title
                })
                .ToListAsync();
            return Ok(new { data, Username = username });
        }
        
        [HttpGet("Get{id}")]
        public async Task<ActionResult<DocumentHeader>> GetDocumentHeaderById(int id)
        {
            var username = User.Identity.Name;
            var data = await _context.DocumentHeaders
                .Include(dh => dh.DocumentLines)
                .FirstOrDefaultAsync(dh => dh.DocumentHeaderId == id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(new { data, Username = username });
        }
       
        [HttpPost("CreateHeader")]
        public async Task<ActionResult<DocumentHeader>> PostDocumentWithLines(AddDocumentWithLinesDto documentWithLinesDto)
        {
            var username = User.Identity.Name;
            var documentHeader = new DocumentHeader
            {
                Title = documentWithLinesDto.Title,
                UserId = documentWithLinesDto.UserId
            };

            foreach (var lineDto in documentWithLinesDto.DocumentLines)
            {
                var documentLine = new DocumentLine
                {
                    Content = lineDto.Content,
                    DocumentHeader = documentHeader
                };
                documentHeader.DocumentLines.Add(documentLine);
            }

            _context.DocumentHeaders.Add(documentHeader);
            await _context.SaveChangesAsync();
            var data = CreatedAtAction(nameof(GetDocumentHeaders), new { id = documentHeader.DocumentHeaderId }, documentHeader);
            return Ok(new { data, Username = username });
        }
        [HttpPost("CreateLine{documentId}/lines")]
        public async Task<ActionResult<DocumentLine>> PostDocumentLine(int documentId, AddDocumentLineDto documentLineDto)
        {
            var username = User.Identity.Name;
            var documentHeader = await _context.DocumentHeaders
                .Include(dh => dh.DocumentLines)
                .FirstOrDefaultAsync(dh => dh.DocumentHeaderId == documentId);

            if (documentHeader == null)
            {
                return NotFound();
            }

            var documentLine = new DocumentLine
            {
                Content = documentLineDto.Content,
                DocumentHeaderId = documentId
            };

            _context.DocumentLines.Add(documentLine);
            await _context.SaveChangesAsync();

            var data = CreatedAtAction("GetDocumentLine", new { id = documentLine.DocumentLineId }, documentLine);
            return Ok(new { data, Username = username });
        }
    }
}
