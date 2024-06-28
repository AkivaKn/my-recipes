using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace MyRecipes.Controllers
{
    public class PdfController : Controller
    {
        [HttpPost]
        public IActionResult Create(string htmlString)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(htmlString);
            var pdfBytes = doc.Save();

            return File(pdfBytes,"application/pdf");
        }
    }
}
