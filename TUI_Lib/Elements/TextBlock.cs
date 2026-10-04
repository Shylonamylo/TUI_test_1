using System.Numerics;
using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class TextBlock : TextElement
{
    public Border? Border { get; set; }
    public override bool Selectable => false;
    public override Vec2I Size => Border!=null ? new Vec2I(Text.Length+2, 3) : new Vec2I(Text.Length, 1);
}