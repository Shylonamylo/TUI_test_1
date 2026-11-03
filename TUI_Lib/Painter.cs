using System.Reflection;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib;

public static class Painter
{
    private static Dictionary<Type, IElementRenderer> _renderers = new();
    
    public static void Paint(Element element, Canvas canvas)
    {
        var type = element.GetType();
        
        var renderer = TryGetRenderer(type);
        
        renderer.Render(element, canvas);
    }

    public static void RegisterRenderer<T, TRenderer>()
        where T : Element
        where TRenderer : IElementRenderer, new()
    {
        _renderers[typeof(T)] = new TRenderer();
    }

    public static IElementRenderer TryGetRenderer(Type type)
    {
        if (_renderers.TryGetValue(type, out var elementRenderer))
        {
            return elementRenderer;
        }
        else
        {
            var nameSpace = type.Namespace?.Replace(".Elements", ".Renderers");
            var rendererTypeName = $"{nameSpace}.{type.Name}Renderer";
            var rendererType = type.Assembly.GetType(rendererTypeName);
            
            if (rendererType == null)
                throw new NotSupportedException($"Нет рендерера {rendererTypeName} для {type.Name}");
            
            IElementRenderer renderer = (IElementRenderer)Activator.CreateInstance(rendererType)!;
            _renderers.Add(type, renderer);
            return renderer;
        }
    }
}