using System.Numerics;

namespace TUI_Lib.Elements;

public class Button : Element
{
    public string Label { get; set; } = "Button";
    public event Action? OnClick;
    
    public override bool Selectable => true;
    
    public Border Border { get; set; } = new();
    
    public override bool HandleKey(ConsoleKeyInfo key){
        
        if (key.Key != ConsoleKey.Enter) return false;
        
        OnClick?.Invoke();
        return true;
    }
    
    public Button()
    {
        
    }
    public Button(string label)
    {
        Label = label;
    }
    public Button(string label, Vector2 position)
    {
        Label = label;
        Position = position;
    }
    public Button(string label, Border border)
    {
        Label = label;
        Border = border;
    }

    public Button(string label, Border border, Vector2 position)
    {
        Label = label;
        Border = border;
        Position = position;
    }
}