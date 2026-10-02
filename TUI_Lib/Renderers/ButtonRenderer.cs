using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class ButtonRenderer : IElementRenderer
{
    public void Render(Element element)
    {
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y);
        
        Button button = (Button)element;

        if (element.Selected)
        {
            using (ColorInverter.Inverted())
            {
                Write(button);
            }
        }
        else
        {
            Write(button);
        }
    }

    private void Write(Button button)
    {
        for (int i = 0; i < button.Label.Length + 2; i++)
        {
            Console.Write(button.Border.HorizontalChar);
        }
        
        Console.Write("\n");
        Console.SetCursorPosition((int)button.Position.X, (int)button.Position.Y+1);

        Console.Write(button.Border.VerticalChar);
        
        Console.Write(button.Label);
        
        Console.WriteLine(button.Border.VerticalChar);
        
        Console.SetCursorPosition((int)button.Position.X, (int)button.Position.Y+2);
        
        for (int i = 0; i < button.Label.Length + 2; i++)
        {
            Console.Write(button.Border.HorizontalChar);
        }
    }
}