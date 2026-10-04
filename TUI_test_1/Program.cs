using System.Numerics;
using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;

namespace TUI_test_1;

class Program
{
    static void Main(string[] args)
    {
        TUI ui = new TUI();

        Button button = new Button
        {
            Position = new Vector2(3, 3), 
            Label = "Нажми на меня!", 
            Border = new Border(), 
            Style = Style.Default
        };
        
        Button button2 = new Button
        {
            Position = new Vector2(3, 6), 
            Label = "Нажми на меня2!", 
            Border = new Border(), 
            Style = Style.Default
        };
        
        TextBlock textBlock = new TextBlock
        {
            Border = new Border(), 
            Position = new Vector2(0, 0), 
            Text = "Привет мир!",
            Style = Style.Default 
        };

        TextBox textBox = new TextBox()
        {
            Position = new Vector2(0, 9),
            Border = new Border(),
            Style = Style.Default
        };
            
        button.OnClick += () =>
        {
            Console.Clear();
        };

        textBox.OnKeyDown += (ConsoleKey key) =>
        {
            if (key == ConsoleKey.Enter)
            {
                textBlock.Text = textBox.Text;
            }
        };
        
        ui.AddElement(textBlock);
        ui.AddElement(button);
        ui.AddElement(button2);
        ui.AddElement(textBox);
        
        ui.Run();
    }
}