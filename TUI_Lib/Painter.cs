using System.Reflection;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib;

public static class Painter
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    
    private static Dictionary<Type, IElementRenderer> _renderers = new();
    
    public static void Paint(Element element, Canvas canvas)
    {
        var type = element.GetType();
        try
        {
            var renderer = TryGetRenderer(type, canvas);
            
            renderer.Render(element);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static IElementRenderer TryGetRenderer(Type type, Canvas canvas)
    {
        if (_renderers.TryGetValue(type, out var elementRenderer))
        {
            return elementRenderer;
        }
        else
        {
            var nameSpace = type.Namespace?.Replace(".Elements", ".Renderers");
            var rendererType = _assembly.GetType($"{nameSpace}.{type.Name}Renderer");
            IElementRenderer renderer = (IElementRenderer)Activator.CreateInstance(rendererType, canvas)!;
            _renderers.Add(type, renderer);
            return renderer;
        }
    }
}