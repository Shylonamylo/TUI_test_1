using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextBoxRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        TextBox textBox = (TextBox)element;
        
        int linesCount = textBox.Text.Length / textBox.MaxLength + 1;
        
        int boxHeight = textBox.Text.Length % textBox.MaxLength == 0 && textBox.Text.Length > 0
            ? textBox.Size.Y + 1
            : textBox.Size.Y;
        
        int textOffset = boxHeight - linesCount < 0 ? 0 : boxHeight - linesCount;

        canvas.DrawBorder(textBox.Position.X, textBox.Position.Y, textBox.Selected?textBox.Size.X+3:textBox.Size.X+2,  boxHeight, textBox.Border, textBox.Style);
        
        canvas.WriteStringWrapped(textBox.Position.X+1, textBox.Position.Y+textOffset-1, textBox.Text, textBox.MaxLength, textBox.Style);

        if (!textBox.Selected) return;
        
        int cursorPos = textBox.GetCursorPos();

        canvas.Set(textBox.Position.X + 1 + cursorPos % textBox.MaxLength,
            textBox.Position.Y + 1 + cursorPos / textBox.MaxLength,
            textBox.GetChar(cursorPos), textBox.Style.GetInverted());

    }
}