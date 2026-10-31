using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class UserCanvasRenderer : IElementRenderer
{
    public void Render(Element element, Canvas canvas)
    {
        UserCanvas userCanvas = (UserCanvas)element;
        
        canvas.DrawBorder(userCanvas.Position.X, userCanvas.Position.Y, userCanvas.Size.X, userCanvas.Size.Y, userCanvas.Border, userCanvas.Style);

        for (int y = 0; y < userCanvas.Height; y++) 
        {
            for (int x = 0; x < userCanvas.Width; x++) 
            {
                canvas.SetCell(userCanvas.Position.X + x + 1, userCanvas.Position.Y + y + 1, userCanvas.GetCell(x, y).Char, userCanvas.GetCell(x, y).Style);
            }
        }
    }
}