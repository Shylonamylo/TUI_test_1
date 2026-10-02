namespace TUI_Lib.ConsoleGraphics.Colors;

public class ColorInverter
{
    private static void Flip()
    {
        (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
    }

    public static IDisposable Inverted()
    {
        return new Inverter();
    }

    private sealed class Inverter : IDisposable
    {
        private bool _disposed = false;
        
        public Inverter()
        {
            Flip();
        }
        
        public void Dispose()
        {
            if(_disposed) return;
            _disposed = true;
            Flip();
        }
    }
}