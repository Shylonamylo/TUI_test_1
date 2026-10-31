using System.Diagnostics;
using TUI_Lib.ConsoleGraphics;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;

namespace TUI_Lib;

public class TUI
{
    private CancellationTokenSource cts = new();

    public event Action<double>? OnTick;
    public event Action<ConsoleKeyInfo>? OnKeyAvalible;
    
    public int SelectedIndex = 0;
    
    public int RequiredFPS { get; set; } = 60;
    
    public int FPS { get; private set; } = 0;

    private readonly Canvas _canvas = new Canvas(Console.WindowWidth, Console.WindowHeight);
    
    private readonly List<Element> _elements = new();

    private int _maxSelectableId = -1;
    private int _minSelectableId = 0;
    
    public TUI()
    {
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        SelectElement(_minSelectableId);
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        stopwatch.Start();
        
        while (!cts.IsCancellationRequested)
        {

            var startTime = stopwatch.Elapsed.TotalSeconds;

            if (SizeChanged())
            {
                _canvas.Resize(Console.WindowWidth, Console.WindowHeight);
                
                try
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                }
                catch
                {
                    // ignored
                }
            }
            
            while(Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                
                OnKeyAvalible?.Invoke(key);
                
                HandleKey(key);
            }
            
            Render();

            var frameTime = stopwatch.Elapsed.TotalSeconds - startTime;
            var sleepTime = (1.0 / RequiredFPS) - frameTime;

            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime*1000));
            }
            
            var deltaTime = stopwatch.Elapsed.TotalSeconds - startTime;
            
            OnTick?.Invoke(deltaTime);

            FPS = (int)(1.0 / deltaTime);

        }
    }

    private bool SizeChanged()
    {
        return (_canvas.Width != Console.WindowWidth || _canvas.Height != Console.WindowHeight)
               && Console.WindowWidth > 0 
               && Console.WindowHeight > 0;
    }

    private void SelectElement(int index)
    {
        _elements[SelectedIndex].Selected = false;
        _elements[index].Selected = true;
        SelectedIndex = index;
    }
    
    private void MoveSelection(int delta)
    {
        var index = -1;
        if (delta > 0)
        {
            for (int i = SelectedIndex+delta; i < _elements.Count; i++)
            {
                if (_elements[i].Selectable)
                {
                    index = i;
                    break;
                }
            }
        }else if (delta < 0)
        {
            for (int i = SelectedIndex+delta; i >= 0; i--)
            {
                if (_elements[i].Selectable)
                {
                    index = i;
                    break;
                }
            }
        }

        if (index != -1)
        {
            SelectElement(index);
        }
    }

    public void HandleKey(ConsoleKeyInfo key)
    {
        if(_elements[SelectedIndex].HandleKey(key)) return;
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                MoveSelection(-1);
                break;
            case ConsoleKey.DownArrow:
                MoveSelection(1);
                break;
        }
    }
    
    public void AddElement(Element element)
    {
        _elements.Add(element);
        if (element.Selectable)
        {
            if (_maxSelectableId == -1)
            {
                _minSelectableId = _elements.Count-1;
            }
            _maxSelectableId = _elements.Count-1;
        }
    }

    public void Render()
    {
        foreach (var element in _elements)
        {
            Painter.Paint(element, _canvas);
        }
        
        Renderer.Draw(_canvas);
    }

    public void Stop()
    {
        cts.Cancel();
        cts.Dispose();
        Console.ReadLine();
    }
}