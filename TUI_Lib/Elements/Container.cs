namespace TUI_Lib.Elements;

public abstract class Container : Element
{
    public List<Element> Elements { get; private set; } = new();

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
            if (Elements.Count > 0)
            {
                Elements[SelectedIndex].Selected = value;
            }
        }
    }

    public int SelectedIndex { get; private set; }

    public Border? Border { get; set; }

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if(Elements[SelectedIndex].HandleKey(key)) return true;
        
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
        Elements.Add(element);
        if (element.Selectable)
        {
            if (MaxSelectableId == -1)
            {
                MinSelectableId = Elements.Count-1;
                SelectElement(MinSelectableId);
            }
            MaxSelectableId = Elements.Count-1;
        }
    }
    
    private void SelectElement(int index)
    {
        if (index >= 0 && Elements.Count > 0)
        {
            Elements[SelectedIndex].Selected = false;
            Elements[index].Selected = true;
            SelectedIndex = index;
        }
    }

    protected bool MoveSelection(int delta)
    {
        var index = -1;
        if (delta > 0)
        {
            for (int i = SelectedIndex+delta; i < Elements.Count; i++)
            {
                if (Elements[i].Selectable)
                {
                    index = i;
                    break;
                }
            }
        }
        else if (delta < 0)
        {
            for (int i = SelectedIndex+delta; i >= 0; i--)
            {
                if (Elements[i].Selectable)
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