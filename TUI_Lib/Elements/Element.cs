using System.Numerics;
using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.Elements;

public abstract class Element
{
    public int Id { get; set; }
    public Vector2 Position { get; set; }
    public virtual bool Selectable { get; } = true;
    public bool Selected { get; set; }
    public Style Style { get; set; } = new Style(ConsoleColor.White, ConsoleColor.Black);
    
    public virtual bool HandleKey(ConsoleKeyInfo key) => false;
}