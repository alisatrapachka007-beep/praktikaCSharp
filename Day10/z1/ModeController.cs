public class ModeController
{
    private static ModeController? _instance;
    private static readonly object _lock = new object();
    private string _mode = "обычный";

    private ModeController() { }

    public static ModeController Instance
    {
        get
        {
            lock (_lock)
            {
                return _instance ??= new ModeController();
            }
        }
    }

    public void SetMode(string mode)
    {
        _mode = mode;
    }

    public string GetMode()
    {
        return _mode;
    }
}
