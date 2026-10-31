using System.Numerics;
using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public class Button : Element
{
    public string Label { get; set; } = "Button";
    public event Action? OnClick;

    public override bool Selectable => true;

    public Border? Border { get; set; } = Border.Default;
    
    public override Vec2I Size
    {
        get
        {
            if (Border == null)
                return new Vec2I(Label.Length, 1);
            else if (Border.FullSize)
                return new Vec2I(Label.Length + 2, 3);
            else
                return new Vec2I(Label.Length + 2, 1);
        }
    }

    public override bool HandleKey(ConsoleKeyInfo key){
        
        if (key.Key != ConsoleKey.Enter) return false;
        
        OnClick?.Invoke();
        return true;
    }
    
    public Button()
    {
        
    }
}