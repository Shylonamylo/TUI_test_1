using System.Numerics;
using TUI_Lib;
using TUI_Lib.Elements;

namespace TUI_test_1;

class Program
{
    static void Main(string[] args)
    {
        TUI ui = new TUI();

        ui.AddElement(new TextBlock(new Border('-','|'), "Привет мир!", new Vector2(0,0)));
        Button button = new Button("Нажми на меня", new Vector2(0,3));
        ui.AddElement(button);
        Button button2 = new Button("Нажми на меня2", new Vector2(0,6));
        ui.AddElement(button2);

        button.OnClick += () =>
        {
            ui.Stop();
        };
        
        ui.Run();
        
        Console.ReadLine();
    }
}