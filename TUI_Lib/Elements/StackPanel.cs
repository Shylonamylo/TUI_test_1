using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class StackPanel : Container
{
    public bool Horizontal { get; set; } = false;

    public override Vec2I Size => new (Childrens.Max(e => e.Size.X), Childrens.Max(e => e.Size.Y));

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
                
                Childrens[SelectedIndex].Selected = false;
                
                break;
            
            case ConsoleKey.DownArrow:
                if (SelectedIndex < _maxSelectableId)
                {
                    MoveSelection(1);
                    return true;
                }
                
                Childrens[SelectedIndex].Selected = false;
                
                break;
        }
        
        return false;
    }
}