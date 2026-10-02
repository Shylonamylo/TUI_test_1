using System.Numerics;

namespace TUI_Lib;

public abstract class Element
{
    public string Id { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 Position { get; set; }
}