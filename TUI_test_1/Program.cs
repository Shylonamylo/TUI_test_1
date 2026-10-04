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

        Button button = new Button
        {
            Position = new Vec2I(3, 3), 
            Label = "Нажми на меня!", 
            Border = new Border(), 
            Style = Style.Default
        };
        
        Button button2 = new Button
        {
            Position = new Vec2I(3, 6), 
            Label = "Нажми на меня2!", 
            Border = new Border(), 
            Style = Style.Default
        };
        
        TextBlock textBlock = new TextBlock
        {
            Border = new Border(), 
            Position = new Vec2I(0, 0), 
            Text = "Привет мир!",
            Style = Style.Default
        };

        TextBox textBox = new TextBox()
        {
            Position = new Vec2I(0, 9),
            Border = new Border(),
            Style = Style.Default
        };

        Container container = new StackPanel()
        {
            Position = new Vec2I(0, 14),
            Border = new Border(),
            Horizontal = true,
            Style = Style.Default
        };

        TextBlock textBlockContainerTest = new TextBlock()
        {
            Border = new Border(),
            Style = Style.Default,
            Text = "Привет из контейнера"
        };
        TextBlock textBlockContainerTest2 = new TextBlock()
        {
            Border = new Border(),
            Style = Style.Default,
            Text = "Привет из контейнера2"
        };
        
        container.AddChildren(textBlockContainerTest);
        container.AddChildren(textBlockContainerTest2);
            
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

        button2.OnClick += () =>
        {
            textBlock.Text = "12341234";
        };
        
        ui.AddElement(textBlock);
        ui.AddElement(button);
        ui.AddElement(button2);
        ui.AddElement(textBox);
        ui.AddElement(container);
        
        ui.Run();
    }
}