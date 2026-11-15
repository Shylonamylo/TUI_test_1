using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class StackPanelRenderer : IElementRenderer
{
    public void Render(Element element, Canvas canvas)
    {
        StackPanel stackPanel = (StackPanel)element;
        if (stackPanel.Horizontal)
        {
            int offsetX = stackPanel.Position.X;
            int offsetY = stackPanel.Position.Y;
            foreach (var childElement in stackPanel.Elements)
            {
                Element renderedChildElement = childElement;
                Vec2I position = new(offsetX, offsetY);
                renderedChildElement.Position = position;
                
                if(offsetX>canvas.Width){break;}
                
                offsetX += childElement.Size.X + 2;
                Painter.Paint(renderedChildElement, canvas);
            }
        }
        else
        {
            int offsetX = stackPanel.Position.X;
            int offsetY = stackPanel.Position.Y;
            foreach (var childElement in stackPanel.Elements)
            {
                Element renderedChildElement = childElement;
                Vec2I position = new(offsetX, offsetY);
                renderedChildElement.Position = position;
                
                if(offsetY>canvas.Height){break;}
                
                offsetY += childElement.Size.Y;
                Painter.Paint(renderedChildElement, canvas);
            }
        }
    }
}