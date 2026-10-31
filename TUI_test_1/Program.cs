using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;
using TUI_test_1.Examples;

namespace TUI_test_1;

class Program
{
    private static readonly TUI _ui = new TUI()
    {
        RequiredFPS = 100
    };
    
    static void Main(string[] args)
    {
        StackPanel stackPanel = new StackPanel();
        
        TextBlock hello = new TextBlock()
        {
            Text = "Выберите желаемый пример: ",
            MaxLength = 50
        };

        Button basic = new Button()
        {
            Label = "Кнопка, текст бокс и текст блок"
        };

        Button dynamicElementsAddition = new Button()
        {
            Label = "Добавление элементов в процессе работы"
        };

        Button pingPing = new Button()
        {
            Label = "Работа с UserCanvas в виде игры PingPong!"
        };

        basic.OnClick += () =>
        {
            new BasicInterfaceExample().Run();
            _ui.Stop();
        };

        dynamicElementsAddition.OnClick += () =>
        {
            new DynamicElementsExample().Run();
            _ui.Stop();
        };
        
        pingPing.OnClick += () =>
        {
            new PongExample().Run();
            _ui.Stop();
        };
        
        stackPanel.AddElement(hello);
        stackPanel.AddElement(basic);
        stackPanel.AddElement(dynamicElementsAddition);
        stackPanel.AddElement(pingPing);
        
        _ui.AddElement(stackPanel);
        
        _ui.Run();

    }
}