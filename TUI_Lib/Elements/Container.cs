namespace TUI_Lib.Elements;

public abstract class Container : Element
{
    private List<Element> _elements = new();
    public List<Element> Elements => _elements;

    private List<int> _removeList = new();
    
    protected int MaxSelectableId = -1;
    protected int MinSelectableId = -1;
    
    private bool _selected;

    public override bool Selectable => true;

    public override bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (_elements.Count > 0 && SelectedIndex >= 0 && SelectedIndex < _elements.Count)
            {
                _elements[SelectedIndex].Selected = value;
            }
        }
    }
    
    public override void ProcessPendingChanges()
    {
        foreach (var element in _elements)
        {
            element.ProcessPendingChanges();
        }
        
        bool needToRecalculate = false;
        
        if (_removeList.Count > 0)
        {
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
                    _removeList.Clear();
                    return;
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
        
        _removeList.Clear();
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

    public override void Invalidate()
    {
        IsDirty = true;
        base.Invalidate();
    }

    public int SelectedIndex { get; private set; }

    public Border? Border { get; set; }

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if (_elements.Count == 0 || SelectedIndex < 0 || SelectedIndex >= _elements.Count) return false;
        if(_elements[SelectedIndex].HandleKey(key)) return true;
        
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (SelectedIndex > MinSelectableId)
                {
                    MoveSelection(-1);
                    return true;
                }
                break;
            
            case ConsoleKey.DownArrow:
                if (SelectedIndex < MaxSelectableId)
                {
                    MoveSelection(1);
                    return true;
                }
                break;
        }
        
        return false;
    }

    public void AddElement(Element element)
    {
        element.Id = IdGenerator.GetNewId();
        
        element.Parent = this;
        
        _elements.Add(element);
        if (element.Selectable)
        {
            RecalculateIndexes();
            SelectElement(MinSelectableId);
        }
        Invalidate();
    }

    public void RemoveElement(Element element)
    {
        if (_elements.Contains(element))
        {
            _removeList.Add(element.Id);
        }
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
    
    private void RecalculateIndexes()
    {
        
        MaxSelectableId = -1;
        MinSelectableId = -1;
        
        for (int i = 0; i < _elements.Count; i++)
        {
            if (MinSelectableId == -1)
            {
                if (_elements[i].Selectable)
                {
                    MinSelectableId = i;
                }
            }
            if (_elements[i].Selectable)
            {
                MaxSelectableId = i;
            }
        }
    }

    protected bool MoveSelection(int delta)
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
        }
        else  if (delta < 0)
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

        if (index > -1)
        {
            SelectElement(index);
            return true;
        }

        return false;
    }

}