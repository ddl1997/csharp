
using Spire.Doc.Documents;
using Spire.Pdf;
using System.Diagnostics;
using System.IO;
using WebSupergoo.ABCpdf11;

namespace CSharpTest.Utils
{
    public class PdfHelper
    {
        public static void HtmlToPdf(string html = null)
        {
            Doc doc = new Doc();
            doc.AddImageHtml(html);
            doc.Save("/Temp/2.pdf");
        }
    }
}
