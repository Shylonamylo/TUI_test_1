using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class StackPanel : Container
{
    public bool Horizontal { get; set; } = false;
    
    private Vec2I _cachedSize;

    public override Vec2I Size
    {
        get
        {
            if (IsDirty)
            {
                RecalculateSize();
            }

            return _cachedSize;
        }
    }

    public override bool HandleKey(ConsoleKeyInfo key)
    {
        if(ElementManager.HandleKey(key)) return true;

        if (Horizontal)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                
                    if(ElementManager.MoveSelection(-1)) return true;
                    break;
            
                case ConsoleKey.RightArrow:
                
                    if(ElementManager.MoveSelection(1)) return true;
                    break;
            }
        }
        else
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if(ElementManager.MoveSelection(-1)) return true;
                    break;
            
                case ConsoleKey.DownArrow:
                    if(ElementManager.MoveSelection(1)) return true;
                    break;
            }
        }
        
        return false;
    }

    private void RecalculateSize()
    {
        if (Horizontal)
        {
            _cachedSize = Elements.Count > 0
                ? new Vec2I(Elements.Sum(e => e.Size.X) + Elements.Count, Elements.Max(e => e.Size.Y))
                : new Vec2I(1, 1);
        }
        else
        {
            _cachedSize = Elements.Count > 0
                ? new Vec2I(Elements.Max(e => e.Size.X), Elements.Sum(e => e.Size.Y) + Elements.Count)
                : new Vec2I(1, 1);
        }
    }
}