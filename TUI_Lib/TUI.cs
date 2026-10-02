using TUI_Lib.Elements;

namespace TUI_Lib;

public class TUI
{
    private CancellationTokenSource cts = new();
    private readonly Painter _painter;
    public int SelectedIndex = 0;
    
    private readonly List<Element> _elements = new();
    
    public TUI()
    {
        _painter = new Painter();
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        while (!cts.IsCancellationRequested)
        {
            Render();
            var key = Console.ReadKey(true);
            HandleKey(key);
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
            _painter.Paint(element);
        }
    }

    public void Stop()
    {
        cts.Cancel();
        cts.Dispose();
    }
}