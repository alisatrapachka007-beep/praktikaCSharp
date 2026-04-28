public interface IReportBuilder
{
    void SetTitle(string title);
    void SetContent(string content);
    Report GetReport();
}
