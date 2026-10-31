using System.Numerics;
using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class TextBlock : TextElement
{
    public Border? Border { get; set; } = Border.Default;
    public override bool Selectable => false;
    public override Vec2I Size
    {
        get
        {
            if (Border == null)
                return new Vec2I(MaxLength, GetLinesCount());
            
            else if (Border.FullSize)
                
                return new Vec2I(MaxLength + 2, GetLinesCount()+2);
            
            else 
            
                return new Vec2I(MaxLength + 2, GetLinesCount());
        }
    }
}