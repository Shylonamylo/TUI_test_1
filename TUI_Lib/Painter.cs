namespace TUI_Lib;

public class Painter
{
    public Painter()
    {
        
    }
    
    public void Paint(Element element)
    {
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y);

        if (element is TextElement)
        {
            TextElement textElement = (TextElement)element;
            Console.WriteLine(textElement.Text);
        }
    }
}