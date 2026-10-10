using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class StackPanel : Container
{
    public bool Horizontal { get; set; } = false;

    public override Vec2I Size => Elements.Count > 0 ? new Vec2I(Elements.Max(e => e.Size.X),Elements.Max(e => e.Size.Y)) : new Vec2I(1,1);

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if (Elements.Count == 0)
        {
            return false;
        }
        if (Elements[SelectedIndex].HandleKey(key))
        {
            return true;
        }
        
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                
                if (SelectedIndex > MinSelectableId)
                {
                    if(MoveSelection(-1)) return true;
                }
                
                break;
            
            case ConsoleKey.DownArrow:
                
                if (SelectedIndex < MaxSelectableId)
                {
                    if(MoveSelection(1)) return true;
                }
                
                break;
        }
        
        return false;
    }
}