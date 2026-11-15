namespace TUI_Lib.Elements;

public class TextElement : Element
{
    private string _text = "";
    private int _maxLength = 20;

    public virtual string Text
    {
        get => _text;
        set
        {
            _text = value; 
            Invalidate();
        }
    }

    public int MaxLength
    {
        get => _maxLength;
        set => _maxLength = Math.Clamp(value, 1, int.MaxValue);
    }

    public int GetLinesCount()
    {
        var result = (Text.Length + MaxLength-1) / MaxLength;
        return result == 0 ? 1 : result;
    }
}