using System;

class Program
{
    static void Main()
    {
        ReportDirector director = new ReportDirector();

        IReportBuilder pdfBuilder = new PDFReportBuilder();
        director.Construct(pdfBuilder);
        Report pdfReport = pdfBuilder.GetReport();
        Console.WriteLine(pdfReport.Title);

        IReportBuilder wordBuilder = new WordReportBuilder();
        director.Construct(wordBuilder);
        Report wordReport = wordBuilder.GetReport();
        Console.WriteLine(wordReport.Title);

        IReportBuilder excelBuilder = new ExcelReportBuilder();
        director.Construct(excelBuilder);
        Report excelReport = excelBuilder.GetReport();
        Console.WriteLine(excelReport.Title);
    }
}