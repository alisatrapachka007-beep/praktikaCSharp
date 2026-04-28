public class PDFReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetTitle(string title) { _report.Title = $"PDF {title}"; }
    public void SetContent(string content) { _report.Content = content; }
    public Report GetReport() { return _report; }
}
