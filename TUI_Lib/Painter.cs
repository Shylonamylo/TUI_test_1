using System.Reflection;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib;

public class Painter
{
    private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    
    public void Paint(Element element)
    {
        var type = element.GetType();
        var nameSpace = type.Namespace?.Replace(".Elements", ".Renderers");
        try
        {
            var rendererType = _assembly.GetType($"{nameSpace}.{type.Name}Renderer");
            
            if (rendererType == null) return;
            
            var renderer = (IElementRenderer)Activator.CreateInstance(rendererType)!;
            renderer.Render(element);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}