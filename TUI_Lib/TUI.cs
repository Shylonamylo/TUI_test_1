using TUI_Lib.ConsoleGraphics;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;

namespace TUI_Lib;

public class TUI
{
    private CancellationTokenSource cts = new();
    public int SelectedIndex = 0;

    private readonly Canvas _canvas = new Canvas(Console.WindowWidth, Console.WindowHeight);
    
    private readonly List<Element> _elements = new();
    
    public TUI()
    {
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        
        while (!cts.IsCancellationRequested)
        {
            Render();
        
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                HandleKey(key);
            }

            Thread.Sleep(16);
        }
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
            for (int i = SelectedIndex+1; i < _elements.Count; i++)
            {
                if (_elements[i].Selectable)
                {
                    index = i;
                    break;
                }
            }
        }else if (delta < 0)
        {
            for (int i = SelectedIndex-1; i > 0; i--)
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
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                MoveSelection(-1);
                break;
            case ConsoleKey.DownArrow:
                MoveSelection(1);
                break;
            case ConsoleKey.Enter:
                _elements[SelectedIndex].HandleKey(key);
                break;
            default:
                if (SelectedIndex >= 0)
                    _elements[SelectedIndex].HandleKey(key);
                break;
        }
    }
    
    public void AddElement(Element element)
    {
        _elements.Add(element);
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
    }
}