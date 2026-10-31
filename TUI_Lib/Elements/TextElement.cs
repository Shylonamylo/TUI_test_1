namespace TUI_Lib.Elements;

public class TextElement : Element
{
    public virtual string Text { get; set; } = "";
    public int MaxLength { get; set; } = 20;
    
    public int GetLinesCount()
    {
        var result = (Text.Length + MaxLength-1) / MaxLength;
        return result == 0 ? 1 : result;
    }
}