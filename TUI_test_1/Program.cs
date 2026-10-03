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
    
        Style style = new Style(ConsoleColor.White, ConsoleColor.Black);
        Button button = new Button{Position = new Vector2(3, 3), Label = "Нажми на меня!", Border = new Border(), Style = style};
        Button button2 = new Button{Position = new Vector2(3, 6), Label = "Нажми на меня2!", Border = new Border(), Style = style};
        TextBlock textBlock = new TextBlock{ Border = new Border(), Position = new Vector2(0, 0), Style = style, Text = "Привет мир!" };
            
        button.OnClick += () =>
        {
            Console.Clear();
        };
        
        ui.AddElement(textBlock);
        ui.AddElement(button);
        ui.AddElement(button2);
        
        ui.Run();
    }
}