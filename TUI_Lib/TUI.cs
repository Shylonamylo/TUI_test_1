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
    public event Action<ConsoleKeyInfo>? OnKeyAvailable;

    private List<int> _removeList = new();
    private List<Element> _addList = new();
    
    public int SelectedIndex { get; private set; } = 0;
    
    public int RequiredFPS { get; set; } = 60;
    
    public int FPS { get; private set; } = 0;

    private readonly Canvas _canvas = new Canvas(Console.WindowWidth, Console.WindowHeight);
    
    private readonly List<Element> _elements = new();

    private int _maxSelectableId = -1;
    private int _minSelectableId = -1;
    
    public TUI()
    {
    }
    
    public void Run()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.CursorVisible = false;

        SelectElement(_minSelectableId);
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        ProcessRemoveList();
        ProcessAddList();
        RecalculateIndexes();
        SelectElement(_minSelectableId);
        
        stopwatch.Start();
        
        while (!cts.IsCancellationRequested)
        {
            var startTime = stopwatch.Elapsed.TotalSeconds;

            CheckSizeChanged();

            ProcessRemoveList();
            ProcessAddList();

            ProcessConsoleKeyBuffer();

            UpdateElements();
            RenderElements();

            var frameTime = stopwatch.Elapsed.TotalSeconds - startTime;
            var sleepTime = (1.0 / RequiredFPS) - frameTime;

            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime*1000));
            }
            
            frameTime = stopwatch.Elapsed.TotalSeconds - startTime;
            
            OnTick?.Invoke(frameTime);
            
            frameTime = stopwatch.Elapsed.TotalSeconds - startTime;

            FPS = (int)(1.0 / frameTime);
        }
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

    private void ProcessRemoveList()
    {
        if (_removeList.Count > 0)
        {
            bool needToRecalculate = false;
            foreach (var element in _removeList)
            {
                Element? elementToRemove = _elements.Find(e => e.Id == element);

                if (elementToRemove == null)
                {
                    continue;
                }
                
                int indexToRemove = _elements.IndexOf(elementToRemove);
                    
                _elements.Remove(elementToRemove);
                
                if (_elements.Count == 0) {
                    SelectedIndex = -1;
                    break;
                }

                if (SelectedIndex > indexToRemove)
                {
                    SelectedIndex = Math.Clamp(SelectedIndex-1, 0, _elements.Count - 1);
                }
                
                if (elementToRemove.Selected)
                {
                    needToRecalculate = true;
                }
            }
            if (needToRecalculate)
            {
                RecalculateIndexes();
                    
                int nearestSelectableElement = SearchNearestSelectableElement(SelectedIndex);

                if (nearestSelectableElement != -1)
                {
                    SelectElement(nearestSelectableElement);
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        _removeList.Clear();
    }

    private void ProcessAddList()
    {
        if (_addList.Count > 0)
        {
            bool needToRecalculate = false;
            
            foreach (var element in _addList)
            {
                if(_elements.Contains(element) || _elements.Any(e => e.Id == element.Id)) continue;
                
                _elements.Add(element);
                
                if (element.Selectable || SelectedIndex==-1)
                {
                    needToRecalculate = true;
                }
            }
            
            if (needToRecalculate)
            {
                RecalculateIndexes();
                    
                int nearestSelectableElement = SearchNearestSelectableElement(SelectedIndex);

                if (nearestSelectableElement != -1)
                {
                    SelectElement(nearestSelectableElement);
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        _addList.Clear();
    }
    
    private void CheckSizeChanged()
    {
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
    }

    private int SearchNearestSelectableElement(int pos)
    {
        if (_elements.Count == 0) return -1;
        
        pos = Math.Clamp(pos, 0, _elements.Count - 1); 
        
        int nearestSelectableElement = -1;
        
        for (int i = 0; i < _elements.Count; i++)
        {
            if (pos - i >= 0)
            {
                if (_elements[pos - i].Selectable)
                {
                    nearestSelectableElement = pos - i;
                    break;
                }
            }
            if (pos + i < _elements.Count)
            {
                if (_elements[pos + i].Selectable)
                {
                    nearestSelectableElement = pos + i;
                    break;
                }
            }
        }

        return nearestSelectableElement;
    }

    private bool SizeChanged()
    {
        return (_canvas.Width != Console.WindowWidth || _canvas.Height != Console.WindowHeight)
               && Console.WindowWidth > 0 
               && Console.WindowHeight > 0;
    }

    private void SelectElement(int index)
    {
        if (index >= 0 && _elements.Count > 0 && index < _elements.Count)
        { 
            if (SelectedIndex >= 0 && SelectedIndex < _elements.Count)
            {
                _elements[SelectedIndex].Selected = false;
            }
            _elements[index].Selected = true;
            SelectedIndex = index;
        }
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
        if (_elements.Count > 0 && SelectedIndex >= 0 && SelectedIndex < _elements.Count)
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
    }
    
    private void RecalculateIndexes()
    {
        _maxSelectableId = -1;
        _minSelectableId = -1;
        
        for (int i = 0; i < _elements.Count; i++)
        {
            if (_minSelectableId == -1)
            {
                if (_elements[i].Selectable)
                {
                    _minSelectableId = i;
                }
            }
            if (_elements[i].Selectable)
            {
                _maxSelectableId = i;
            }
        }
    }
    
    public void AddElement(Element element)
    {
        element.Id = IdGenerator.GetNewId();
        _addList.Add(element);
    }

    public void UpdateElements()
    {
        foreach (var element in _elements)
        {
            element.ProcessPendingChanges();
        }
    }
    
    public void RenderElements()
    {
        foreach (var element in _elements)
        {
            Painter.Paint(element, _canvas);
        }
        
        Renderer.Draw(_canvas);
    }

    public void RemoveElement(Element element)
    {
        _removeList.Add(element.Id);
    }

    public void Stop()
    {
        cts.Cancel();
        cts.Dispose();
    }
}