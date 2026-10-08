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

    private int _maxSelectableId = -1;
    private int _minSelectableId = -1;
    
    public TUI()
    {
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        SelectElement(_minSelectableId);
        
        while (!cts.IsCancellationRequested)
        {
            while(Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                HandleKey(key);
            }
            
            Render();

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
    }
}