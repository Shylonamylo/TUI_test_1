namespace TUI_Lib.Elements;

public abstract class Container : Element
{
    public List<Element> Childrens { get; set; } = new();
    
    public Border? Border { get; set; }

    public void AddChildren(Element child)
    {
        Childrens.Add(child);
    }
}