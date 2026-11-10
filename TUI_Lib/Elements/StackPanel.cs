using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class StackPanel : Container
{
    public bool Horizontal { get; set; } = false;
    
    private Vec2I _cachedSize = new Vec2I();

    public override Vec2I Size
    {
        get
        {
            if (IsDirty)
            {
                recalculateSize();
                IsDirty = false;
            }

            return _cachedSize;
        }
    }

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

        if (Horizontal)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                
                    if (SelectedIndex > MinSelectableId)
                    {
                        if(MoveSelection(-1)) return true;
                    }
                
                    break;
            
                case ConsoleKey.RightArrow:
                
                    if (SelectedIndex < MaxSelectableId)
                    {
                        if(MoveSelection(1)) return true;
                    }
                
                    break;
            }
        }
        else
        {
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
        }
        
        return false;
    }

    private void recalculateSize()
    {
        if (Horizontal)
        {
            _cachedSize = Elements.Count > 0
                ? new Vec2I(Elements.Sum(e => e.Size.X), Elements.Max(e => e.Size.Y))
                : new Vec2I(1, 1);
        }
        else
        {
            _cachedSize = Elements.Count > 0
                ? new Vec2I(Elements.Max(e => e.Size.X), Elements.Sum(e => e.Size.Y))
                : new Vec2I(1, 1);
        }
    }
}