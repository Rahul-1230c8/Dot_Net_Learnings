using Microsoft.AspNetCore.Mvc;
using Rahul.InvoiceDocument.Api.Models;
using System.Net.Http.Json;

namespace Rahul.InvoiceDocument.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceDocumentsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InvoiceDocumentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoice([FromBody] InvoiceRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync(
                "https://localhost:7100/api/document/generate",
                request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var fileBytes = await response.Content.ReadAsByteArrayAsync();

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "Invoice.docx");
        }
    }
}