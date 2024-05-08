using Microsoft.AspNetCore.Mvc;
using IronOcr;
using System.Text;

namespace ManualTroubleShooter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        public PdfController()
        {
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            License.LicenseKey = "IRONSUITE.RAVIKIRANEDARA.LONDON.GMAIL.COM.26558-5ABDB60E7C-CHEBXHM-5X3FF72QYEAF-Y5L7AHGDOYCB-4Y62P3QVK2UA-L4YF5IGYEIYC-MFUBIZ3MNUS6-KE45IQR6HOBD-ZGF7OK-TCU4CTGYNJCMUA-DEPLOYMENT.TRIAL-ROOXDY.TRIAL.EXPIRES.06.JUN.2024";

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Save the uploaded file to a temporary location
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Initialize IronOCR
            var ocr = new IronTesseract();

            // Read text from PDF
            using (var ocrInput = new OcrInput())
            {
                ocrInput.LoadPdf(filePath);
                var ocrResult = ocr.Read(ocrInput);

                // Extract troubleshooting information from pages, ignore 'TroubleShooting' keyword if it is not followed up with 'problem' keyword instructions. 
                var troubleShootingPages = ocrResult.Pages.Where(page => ((page.Contents.IndexOf("TroubleShooting", StringComparison.OrdinalIgnoreCase) >= 0) &&
                                                                          (page.Contents.IndexOf("problem", StringComparison.OrdinalIgnoreCase) >= 0))
                                                                        ||
                                                                        ((page.Contents.IndexOf("problem", StringComparison.OrdinalIgnoreCase) >= 0) &&
                                                                          (page.Contents.IndexOf("solution", StringComparison.OrdinalIgnoreCase) >= 0))
                                                                 ).Select(pg => pg);

                if (troubleShootingPages != null && troubleShootingPages.Any())
                {
                    // Build summary
                    StringBuilder summary = new StringBuilder();
                    string pageNumbers = string.Empty;

                    foreach (var page in troubleShootingPages)
                    {
                        summary.AppendLine(FormatContent(page.Contents)).Replace("\r\n", " ");
                        pageNumbers = pageNumbers + "," + page.PageNumber;
                    }

                    // Return extracted trouble shooting section.
                    return Ok(new ManualSection
                    {
                        FileName = file.FileName,
                        Name = Constants.TroubleShooting,
                        PageNumber = pageNumbers.TrimStart(','),
                        Summary = summary.ToString()
                    });
                }
            }
            return NotFound();
        }

        private string FormatContent(string content)
        {
            //sometimes TroubleShooting can be repeated for different purposes but we need the actual section where it mentions the problem and its solutions.
            var keyWordFoundAt = content.LastIndexOf("TroubleShooting", StringComparison.OrdinalIgnoreCase) > 0
                                  ? content.LastIndexOf("TroubleShooting", StringComparison.OrdinalIgnoreCase)
                                  : content.IndexOf("problem", StringComparison.OrdinalIgnoreCase);

            return content.Substring(keyWordFoundAt).Replace("\r\n", " ");
        }

    }
}
