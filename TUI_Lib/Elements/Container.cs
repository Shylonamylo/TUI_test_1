namespace TUI_Lib.Elements;

public abstract class Container : Element
{
    public IReadOnlyList<Element> Elements => ElementManager.GetElements();
    
    protected readonly ElementManager ElementManager = new();
    
    public Border? Border { get; set; }
    
    private bool _selected;

    public override bool Selectable => true;

    public override bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (value)
            {
                ElementManager.SelectElement(ElementManager.SelectedIndex);
            }
            else
            {
                ElementManager.DeselectCurrent();
            }
        }
    }
    
    public override void ProcessPendingChanges()
    {
        foreach (var element in Elements)
        {
            element.ProcessPendingChanges();
        }

        ElementManager.Update();
    }

    public override void Invalidate()
    {
        IsDirty = true;
        base.Invalidate();
    }

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if(ElementManager.HandleKey(key)) return true;
        
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if(ElementManager.MoveSelection(-1)) return true;
                break;
            
            case ConsoleKey.DownArrow:
                if(ElementManager.MoveSelection(1)) return true;
                break;
        }
        
        return false;
    }

    public void AddElement(Element element)
    {
        ElementManager.AddElement(element, this);
    }

    public void RemoveElement(Element element)
    {
        ElementManager.RemoveElement(element);
    }

}