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
        
        ui.AddElement(new TextBlock{Border = new Border(), Position = new Vector2(0, 0), Style = style, Text = "Привет мир!"});
        
        Button button = new Button{Position = new Vector2(3, 3), Label = "Нажми на меня!", Border = new Border(), Style = style};
        
        ui.AddElement(button);
        
        Button button2 = new Button{Position = new Vector2(3, 6), Label = "Нажми на меня2!", Border = new Border(), Style = style};
        
        ui.AddElement(button2);

        button.OnClick += () =>
        {
            Console.Clear();
        };
        
        ui.Run();
        
        Console.ReadLine();
    }
}