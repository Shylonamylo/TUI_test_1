using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;
using Timer = System.Timers.Timer;

namespace TUI_Lib.Renderers;

public class TextBoxRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        TextBox textBox = (TextBox)element;

        canvas.DrawBox((int)textBox.Position.X, (int)textBox.Position.Y, textBox.Selected?textBox.Text.Length+3:textBox.Text.Length+2, 3, textBox.Border.HorizontalChar, textBox.Border.VerticalChar, textBox.Style);
        
        canvas.WriteString((int)textBox.Position.X+1, (int)textBox.Position.Y+1, textBox.Text, textBox.Style);

        if (!textBox.Selected) return;
        
        int cursorPos = textBox.GetCursorPos();

        canvas.Set((int)textBox.Position.X+cursorPos+1, (int)textBox.Position.Y+1, textBox.GetChar(cursorPos), textBox.Style.GetInverted());

    }
}