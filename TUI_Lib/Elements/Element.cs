using System.Numerics;

namespace TUI_Lib.Elements;

public abstract class Element
{
    public int Id { get; set; }
    public Vector2 Position { get; set; }
    public virtual bool Selectable { get; } = true;
    public bool Selected { get; set; }
    
    public virtual bool HandleKey(ConsoleKeyInfo key) => false;
}