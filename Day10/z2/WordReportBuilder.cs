public class WordReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    public void SetTitle(string title) { _report.Title = $"Word {title}"; }
    public void SetContent(string content) { _report.Content = content; }
    public Report GetReport() { return _report; }
}
