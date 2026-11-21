using System.Diagnostics;
using System.Runtime.InteropServices;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;

namespace TUI_Lib;

public class TUI
{
    private CancellationTokenSource cts = new();

    public event Action<double>? OnTick;
    public event Action<ConsoleKeyInfo>? OnKeyAvailable;

    public ElementManager ElementManager { get; } = new();
    
    public int RequiredFPS
    {
        get => _requiredFps;
        set => _requiredFps = Math.Clamp(value, 1, int.MaxValue);
    }

    public int FPS { get; private set; } = 0;

    private Renderer? _renderer;

    private Canvas? _canvas;

    private int _requiredFps = 60;

    private bool _stopped = false;

    public void Run()
    {
        ConsoleInit();

        _canvas = new Canvas(Console.WindowWidth, Console.WindowHeight);
        
        _renderer = new Renderer();
        
        _renderer?.Reset();

        ElementManager.Init();
        
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        Exception? crash = null;

        try
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var startTime = stopwatch.Elapsed.TotalSeconds;

                    CheckSizeChanged();

                    ElementManager.Update();

                    ProcessConsoleKeyBuffer();

                    ElementManager.UpdateElements();

                    ElementManager.RenderElements(_renderer!, _canvas!);

                    var frameTime = stopwatch.Elapsed.TotalSeconds - startTime;
                    var sleepTime = (1.0 / RequiredFPS) - frameTime;

                    if (sleepTime > 0)
                    {
                        Thread.Sleep((int)(sleepTime * 1000));
                    }

                    frameTime = stopwatch.Elapsed.TotalSeconds - startTime;

                    OnTick?.Invoke(frameTime);

                    frameTime = stopwatch.Elapsed.TotalSeconds - startTime;

                    FPS = frameTime > 0 ? (int)(1.0 / frameTime) : RequiredFPS;
                }
                catch (Exception e)
                {
                    crash = e;
                    Stop();
                }
            }
        }
        finally
        {
            Console.CancelKeyPress -= OnCancel;
            _renderer?.Reset();
            Console.Write("\x1b[0m\x1b[?25h\x1b[?1049l");
            if (crash != null)
            {
                Console.WriteLine(crash);
            }
        }
    }

    private void ConsoleInit()
    {
        if (Console.IsOutputRedirected)
            throw new InvalidOperationException("TUI нужен интерактивный терминал");

        if (OperatingSystem.IsWindows())
        {
            const int stdOutputHandle = -11;
            const uint enableVirtualTerminalProcessing = 4;

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll")]
            static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

            [DllImport("kernel32.dll")]
            static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

            void SetupConsole()
            {
                IntPtr handle = GetStdHandle(stdOutputHandle);
                GetConsoleMode(handle, out var mode);
                mode |= enableVirtualTerminalProcessing;
                SetConsoleMode(handle, mode);
            }

            SetupConsole();
        }
        
        Console.CancelKeyPress += OnCancel;
        
        Console.Write("\x1b[?1049h\x1b[?25l\x1b[2J\x1b[H");
    }

    private void OnCancel(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Stop();
    }

    private void ProcessConsoleKeyBuffer()
    {
        while(Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
                
            OnKeyAvailable?.Invoke(key);

            HandleKey(key);
        }
    }

    private void HandleKey(ConsoleKeyInfo key)
    {
        if (!ElementManager.HandleKey(key))
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    ElementManager.MoveSelection(-1);
                    break;
                case ConsoleKey.DownArrow:
                    ElementManager.MoveSelection(1);
                    break;
            }
        }
    }

    private void CheckSizeChanged()
    {
        if (SizeChanged())
        {
            _canvas?.Resize(Console.WindowWidth, Console.WindowHeight);
            try
            {
                Console.Write("\x1b[?25l\x1b[2J\x1b[H");
                _renderer?.Reset();
            }
            catch
            {
                //ignored
            }
        }
    }

    private bool SizeChanged()
    {
        return (_canvas?.Width != Console.WindowWidth || _canvas?.Height != Console.WindowHeight)
               && Console.WindowWidth > 0 
               && Console.WindowHeight > 0;
    }
    
    
    public void AddElement(Element element)
    {
        ElementManager.AddElement(element);
    }

    public void RemoveElement(Element element)
    {
        ElementManager.RemoveElement(element);
    }

    public void Stop()
    {
        if(_stopped) return;
        _stopped = true;
        cts.Cancel();
    }
}