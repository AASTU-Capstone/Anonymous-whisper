using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using   ComplaintSystem.Application.Persistence.Contracts.Common;

namespace   ComplaintSystem.Infrastructure.Services;

public class PdfReaderService : IPdfReaderService
{
    public string ReadFromPdf(Stream path)
    {
        PdfReader reader = new PdfReader(path);
        List<string> lines = new List<string>();
        for (int page = 1; page <= reader.NumberOfPages; page++)
        {
            string curr = PdfTextExtractor.GetTextFromPage(reader, page);
            lines.Add(curr);
        }
        string texts = String.Join("", lines);
        return texts;
    }
}
