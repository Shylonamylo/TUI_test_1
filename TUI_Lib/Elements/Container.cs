namespace TUI_Lib.Elements;

public abstract class Container : Element
{
    public List<Element> Childrens { get; private set; } = new();

    protected int _maxSelectableId = -1;
    protected int _minSelectableId = -1;
    private bool _selected;

    public override bool Selectable => true;

    public override bool Selected
    {
        get => _selected;
        set 
        {
            _selected = value;
            Childrens[SelectedIndex].Selected = value;
        }
    }

    public int SelectedIndex { get; private set; }

    public Border? Border { get; set; }

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if(Childrens[SelectedIndex].HandleKey(key)) return true;
        
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (SelectedIndex > _minSelectableId)
                {
                    MoveSelection(-1);
                    return true;
                }
                break;
            
            case ConsoleKey.DownArrow:
                if (SelectedIndex < _maxSelectableId)
                {
                    MoveSelection(1);
                    return true;
                }
                break;
        }
        
        return false;
    }

    public void AddChildren(Element child)
    {
        Childrens.Add(child);
        if (child.Selectable)
        {
            if (_maxSelectableId == -1)
            {
                _minSelectableId = Childrens.Count-1;
                SelectElement(_minSelectableId);
            }
            _maxSelectableId = Childrens.Count-1;
        }
    }
    
    private void SelectElement(int index)
    {
        Childrens[SelectedIndex].Selected = false;
        Childrens[index].Selected = true;
        SelectedIndex = index;
    }

    protected void MoveSelection(int delta)
    {
        var index = -1;
        if (delta > 0)
        {
            for (int i = SelectedIndex+delta; i < Childrens.Count; i++)
            {
                if (Childrens[i].Selectable)
                {
                    index = i;
                    break;
                }
            }
        }else if (delta < 0)
        {
            for (int i = SelectedIndex+delta; i >= 0; i--)
            {
                if (Childrens[i].Selectable)
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

}