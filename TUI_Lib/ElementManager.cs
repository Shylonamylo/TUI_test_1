using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib;

public class ElementManager
{
    private List<int> _removeList = new();
    private List<Element> _addList = new();
    
    private readonly List<Element> _elements = new();
    
    private int _maxSelectableId = -1;
    private int _minSelectableId = -1;

    public int SelectedIndex { get; private set; } = 0;
    
    public void ProcessRemoveList()
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
                    
                elementToRemove.Invalidate();
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

    public void ProcessAddList()
    {
        if (_addList.Count > 0)
        {
            bool needToRecalculate = false;
            
            foreach (var element in _addList)
            {
                if(_elements.Contains(element) || _elements.Any(e => e.Id == element.Id)) continue;
                
                _elements.Add(element);
                element.Invalidate();
                
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
    
    public int SearchNearestSelectableElement(int pos)
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
    
    public bool MoveSelection(int delta)
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
            return true;
        }

        return false;
    }
    
    public void SelectElement(int index)
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

    public void DeselectCurrent()
    {
        if (SelectedIndex >= 0 && SelectedIndex < _elements.Count)
        {
            _elements[SelectedIndex].Selected = false;
        }
    }
    
    public void RecalculateIndexes()
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
    
    public void AddElement(Element element, Container? parent = null)
    {
        element.Id = IdGenerator.GetNewId();
        if (parent != null)
        {
            element.Parent = parent;
        }
        _addList.Add(element);
    }

    public void UpdateElements()
    {
        foreach (var element in _elements)
        {
            element.ProcessPendingChanges();
        }
    }
    
    public void RemoveElement(Element element)
    {
        _removeList.Add(element.Id);
    }
    
    public bool HandleKey(ConsoleKeyInfo key)
    {
        if (_elements.Count > 0 && SelectedIndex >= 0 && SelectedIndex < _elements.Count)
        {
            if(_elements[SelectedIndex].HandleKey(key)) return true;
        }

        return false;
    }
    
    public void RenderElements(Renderer renderer, Canvas canvas)
    {
        foreach (var element in _elements)
        {
            Painter.Paint(element, canvas);
        }
        
        renderer.Draw(canvas);
    }

    public void Update()
    {
        ProcessRemoveList();
        ProcessAddList();
    }

    public void Init()
    {
        ProcessRemoveList();
        ProcessAddList();
        RecalculateIndexes();
        SelectElement(_minSelectableId);
    }

    public IReadOnlyList<Element> GetElements()
    {
        return _elements;
    }
}