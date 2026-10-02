namespace TUI_Lib;

public class TextBlock : TextElement
{
    public Border Border { get; set; } = new Border();
    public TextBlock()
    {
        
    }

    public TextBlock(string text)
    {
        Text = text;
    }

    public TextBlock(char horizontal, char vertical, string text)
    {
        Text = text;
        Border = new Border(horizontal, vertical);
    }
}