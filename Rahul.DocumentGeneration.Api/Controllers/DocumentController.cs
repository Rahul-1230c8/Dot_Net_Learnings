using Microsoft.AspNetCore.Mvc;
using Rahul.DocumentGeneration.Api.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace Rahul.DocumentGeneration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        [HttpPost("generate")]
        public IActionResult Generate([FromBody] InvoiceRequest request)
        {
            var memoryStream = new MemoryStream();

            using (var wordDoc = WordprocessingDocument.Create(
                memoryStream,
                DocumentFormat.OpenXml.WordprocessingDocumentType.Document,
                true))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = new Body();

                body.Append(new Paragraph(new Run(new Text($"Invoice: {request.InvoiceNumber}"))));
                body.Append(new Paragraph(new Run(new Text($"Customer: {request.CustomerName}"))));
                body.Append(new Paragraph(new Run(new Text($"Amount: ${request.Amount}"))));

                mainPart.Document.Append(body);
            }

            memoryStream.Position = 0;

            return File(memoryStream,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "Invoice.docx");
        }
    }
}