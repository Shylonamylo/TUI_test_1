using System.Numerics;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;

namespace TUI_Lib.Elements;

public abstract class Element
{
    public int Id { get; set; }
    public Vec2I Position { get; set; }
    public virtual Vec2I Size { get; set; }
    public virtual bool Selectable { get; set; } = true;
    public virtual bool Selected { get; set; }
    public Style Style { get; set; } = Style.Default;
    
    public virtual bool HandleKey(ConsoleKeyInfo key) => false;
}