namespace TUI_Lib.Elements;

public class StackPanel : Container
{
    public bool Horizontal { get; set; } = false;

    public override bool Selectable => false;
}