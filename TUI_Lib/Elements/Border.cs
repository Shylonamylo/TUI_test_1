namespace TUI_Lib;

public class Border
{
    public char HorizontalChar = '-';
    public char VerticalChar = '|';
    
    public Border()
    {
    }

    public Border(char horizontal, char vertical)
    {
        HorizontalChar = horizontal;
        VerticalChar = vertical;
    }
}