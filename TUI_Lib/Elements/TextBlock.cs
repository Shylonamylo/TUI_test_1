using System.Numerics;

namespace TUI_Lib.Elements;

public class TextBlock : TextElement
{
    public Border Border { get; set; } = new();
    public override bool Selectable => false;
}