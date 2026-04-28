public class ReportDirector
{
    public void Construct(IReportBuilder builder)
    {
        builder.SetTitle("Отчет");
        builder.SetContent("Содержание отчета");
    }
}
