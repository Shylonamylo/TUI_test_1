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

    private static readonly StackPanel _stackPanel = new StackPanel();

    private static readonly TextBlock _hello = new TextBlock()
    {
        Text = "Выберите желаемый пример: ",
        MaxLength = 50
    };

    private static readonly Button _basic = new Button()
    {
        Label = "Кнопка, текст бокс и текст блок"
    };

    private static readonly Button _dynamicElementsAddition = new Button()
    {
        Label = "Добавление элементов в процессе работы"
    };

    private static readonly Button _pingPing = new Button()
    {
        Label = "Работа с UserCanvas в виде игры PingPong!"
    };

    private static readonly Button _performanceTest = new Button()
    {
        Label = "Стресс тест"
    };

    private static readonly Button _customElement = new Button()
    {
        Label = "Кастомный элемент"
    };
    
    private static readonly Button _bubbleSort = new Button()
    {
        Label = "Визуализация пузырьковой сортировки"
    };
    
    static void Main(string[] args)
    {
        _basic.OnClick += () =>
        {
            new BasicInterfaceExample().Run();
            _ui.Stop();
        };

        _dynamicElementsAddition.OnClick += () =>
        {
            new DynamicElementsExample().Run();
            _ui.Stop();
        };
        
        _pingPing.OnClick += () =>
        {
            new PongExample().Run();
            _ui.Stop();
        };

        _performanceTest.OnClick += () =>
        {
            new PerformanceTest().Run();
            _ui.Stop();
        };

        _customElement.OnClick += () =>
        {
            new CustomElementExample().Run();
            _ui.Stop();
        };

        _bubbleSort.OnClick += () =>
        {
            new BubbleSortVisualizationExample().Run();
            _ui.Stop();
        };
        
        _stackPanel.AddElement(_hello);
        _stackPanel.AddElement(_basic);
        _stackPanel.AddElement(_dynamicElementsAddition);
        _stackPanel.AddElement(_pingPing);
        _stackPanel.AddElement(_performanceTest);
        _stackPanel.AddElement(_customElement);
        _stackPanel.AddElement(_bubbleSort);
        
        _ui.AddElement(_stackPanel);
        
        _ui.Run();

    }
}