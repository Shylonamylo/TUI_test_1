using System.Numerics;
using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;

namespace TUI_test_1;

class Program
{
    static void Main(string[] args)
    {
        TUI ui = new TUI();

        StackPanel stackPanel = new StackPanel()
        {
            Horizontal = true,
        };
        
        Button button = new Button()
        {
            Border =  new Border(),
            Label = "Проверка 1",
            Style = Style.Default,
        };

        StackPanel stackPanel2 = new StackPanel();

        TextBox textBox = new TextBox()
        {
            Border = new Border(),
            Style = Style.Default,
        };
        
        Button button2 = new Button()
        {
            Border =  new Border(),
            Label = "Проверка 2",
            Style = Style.Default,
        };
        
        StackPanel stackPanel3 = new StackPanel();

        TextBox textBox2 = new TextBox()
        {
            Border = new Border(),
            Style = Style.Default,
        };
        
        Button button3 = new Button()
        {
            Border =  new Border(),
            Label = "Проверка 3",
            Style = Style.Default,
        };
        
        stackPanel3.AddChildren(button3);
        stackPanel3.AddChildren(textBox2);
        
        stackPanel2.AddChildren(textBox);
        stackPanel2.AddChildren(button2);
        
        stackPanel.AddChildren(button);
        stackPanel.AddChildren(stackPanel2);
        stackPanel.AddChildren(stackPanel3);
        
        ui.AddElement(stackPanel);
        
        ui.Run();
    }
}