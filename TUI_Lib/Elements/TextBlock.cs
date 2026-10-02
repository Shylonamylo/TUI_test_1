using System.Numerics;

namespace TUI_Lib.Elements;

public class TextBlock : TextElement
{
    public override bool Selectable => false;

    public TextBlock()
    {
        
    }

    public TextBlock(string text)
    {
        Text = text;
    }

    public TextBlock(Border border, string text)
    {
        Text = text;
        Border = border;
    }
    public TextBlock(Border border, string text, Vector2 position)
    {
        Text = text;
        Border = border;
        Position = new Vector2(position.X, position.Y);
    }
    public TextBlock(string text, Vector2 position)
    {
        Text = text;
        Position = new Vector2(position.X, position.Y);
    }
}