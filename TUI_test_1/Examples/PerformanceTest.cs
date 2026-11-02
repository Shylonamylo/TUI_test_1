using System.Diagnostics;
using TUI_Lib;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class PerformanceTest
{
    private TUI _ui = new()
    {
        RequiredFPS = 10000000
    };
    
    private StackPanel _mainStackPanel = new();
    private StackPanel _performanceStackPanel = new();
    private StackPanel _maxElementsCountInputStackPanel = new();
    
    private TextBlock _title = new();
    private TextBlock _currentElementsCount = new();
    private TextBlock _currentFps = new();
    private TextBlock _currentRamUsage = new();
    private TextBlock _maxElementsCount = new();
    
    private TextBox _maxElementsCountInput = new();
    
    private Button _startTest = new();

    private int _maxElementsCountInt;
    private int _currentElementsCountInt;
    
    public void Run()
    {
        RecalcAll();
        
        _title.Text = "Проверка производительности";
        _title.MaxLength = _title.Text.Length;

        _startTest.Label = "Начать тест";

        _maxElementsCountInputStackPanel.Horizontal = true;
        
        _maxElementsCountInput.OnKeyDown += (key) =>
        {
            if (key != ConsoleKey.Enter) return;
            
            if (int.TryParse(_maxElementsCountInput.Text, out int parsed))
            {
                _maxElementsCountInt = parsed;
                _maxElementsCount.Text = $"{_maxElementsCountInt}";
            }
        };

        _startTest.OnClick += () =>
        {
            for (int i = 0; i < _maxElementsCountInt; i++)
            {
                _performanceStackPanel.AddElement(new TextBlock()
                {
                    Text = "Привет, я нагружаю этот фреймфорк!!!"
                });

                _currentElementsCountInt++;
            }
        };

        _ui.OnTick += (_) => { RecalcAll(); };
        
        _maxElementsCountInputStackPanel.AddElement(_maxElementsCountInput);
        _maxElementsCountInputStackPanel.AddElement(_maxElementsCount);
        
        _mainStackPanel.AddElement(_title);
        _mainStackPanel.AddElement(_currentFps);
        _mainStackPanel.AddElement(_currentRamUsage);
        _mainStackPanel.AddElement(_currentElementsCount);
        _mainStackPanel.AddElement(_maxElementsCountInputStackPanel);
        _mainStackPanel.AddElement(_startTest);
        _mainStackPanel.AddElement(_performanceStackPanel);
        
        _ui.AddElement(_mainStackPanel);
        _ui.Run();
    }

    private void RecalcAll()
    {
        Process process = Process.GetCurrentProcess();
        
        _currentElementsCount.Text = $"Текущее количество элементов: {_currentElementsCountInt}";
        _currentElementsCount.MaxLength = _currentElementsCount.Text.Length;
        
        _currentFps.Text = $"Текущий ФПС: {_ui.FPS.ToString()}";
        _currentFps.MaxLength = _currentFps.Text.Length;

        _currentRamUsage.Text = $"Текущее потребление ОЗУ: {process.PrivateMemorySize64/1000000f}";
        _currentRamUsage.MaxLength = _currentRamUsage.Text.Length;
    }
}