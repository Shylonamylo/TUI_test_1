namespace TUI_Lib.Elements;

public class Border
{
    public char? HorizontalChar = null;
    public char? VerticalChar = null;
    public char? LeftUpCorner = null;
    public char? RightUpCorner = null;
    public char? LeftDownCorner = null;
    public char? RightDownCorner = null;
    
    public static Border Default => new('-', '|', '┌', '┐', '└', '┘');
    public static Border DefaultFlat => new(null, '|', null, null, null, null);

    public bool FullSize => HorizontalChar != null;

    public Border()
    {
    }

    public Border(char? horizontal, char? vertical, char? leftUp, char? rightUp, char? leftDown, char? rightDown)
    {
        HorizontalChar = horizontal;
        VerticalChar = vertical;
        LeftUpCorner = leftUp;
        RightUpCorner = rightUp;
        LeftDownCorner = leftDown;
        RightDownCorner = rightDown;
    }
}