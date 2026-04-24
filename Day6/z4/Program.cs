using System;

class WaterLevelEventArgs : EventArgs
{
    public int Level { get; set; }
    public WaterLevelEventArgs(int level) { Level = level; }
}

class WaterTankSensor
{
    public event EventHandler<WaterLevelEventArgs> WaterLevelChanged;

    public void SetLevel(int level)
    {
        WaterLevelChanged?.Invoke(this, new WaterLevelEventArgs(level));
    }
}

class WaterMonitor
{
    public WaterMonitor(WaterTankSensor sensor, PumpController pump, WarningSystem warning)
    {
        sensor.WaterLevelChanged += pump.Control;
        sensor.WaterLevelChanged += warning.Warn;
    }
}

class PumpController
{
    public void Control(object sender, WaterLevelEventArgs e)
    {
        if (e.Level > 80)
            Console.WriteLine($"Насос включен (уровень: {e.Level}%)");
        else
            Console.WriteLine($"Насос выключен (уровень: {e.Level}%)");
    }
}

class WarningSystem
{
    public void Warn(object sender, WaterLevelEventArgs e)
    {
        if (e.Level > 90)
            Console.WriteLine($" Переполнение. Уровень {e.Level}%");
    }
}

class Program
{
    static void Main()
    {
        WaterTankSensor sensor = new WaterTankSensor();
        PumpController pump = new PumpController();
        WarningSystem warning = new WarningSystem();

        WaterMonitor monitor = new WaterMonitor(sensor, pump, warning);

        sensor.SetLevel(50);
        sensor.SetLevel(85);
        sensor.SetLevel(95);
    }
}