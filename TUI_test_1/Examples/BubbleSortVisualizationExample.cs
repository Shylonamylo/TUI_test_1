using System.Reflection.Emit;
using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class BubbleSortVisualizationExample
{
    private readonly TUI _ui = new() { RequiredFPS = 100 };
    
    private (int, bool)[] _array = new (int, bool)[60];
    private int _i = 0, _j = 60;
    
    private readonly StackPanel _panel = new();
    private readonly UserCanvas _canvas = new(60, 30);
    private readonly TextBlock _status = new() { MaxLength = 60 };
    private readonly TextBlock _info = new() { MaxLength = 60 };

    private readonly StackPanel _controlPanel = new(){Horizontal = true};
    private Button _startSort = new() {Label = "Старт"};
    private Button _stopSort = new() {Label = "Стоп"};
    private Button _step = new() {Label = "Шаг"};
    private Button _restart = new() {Label = "Перезапустить"};
    
    private bool _isSorted = false;
    private bool _isRunning = false;
    
    public void Run()
    {
        _array = new (int, bool)[60];
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i].Item1 = Random.Shared.Next(1,28);
            _array[i].Item2 = false;
        }
        RenderCanvas();
        
        _info.Text = "Пауза";

        _ui.OnTick += (dt) =>
        {
            if (_isRunning)
            {
                Step();
                RenderCanvas();
            }
        };

        _startSort.OnClick += () =>
        {
            _isRunning = true;
            _info.Text = "Сортируем";
        };
        
        _stopSort.OnClick += () =>
        {
            _isRunning = false;
            _info.Text = "Пауза";
        };
        
        _step.OnClick += () =>
        {
            Step();
            RenderCanvas();
        };
        
        _restart.OnClick += () =>
        {
            _array = new (int, bool)[60];
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i].Item1 = Random.Shared.Next(1,28);
                _array[i].Item2 = false;
            }

            _i = 0;
            _j = 60;

            _status.Text = "";
            _info.Text = "Пауза";

            _isSorted = false;
            
            RenderCanvas();
        };
        
        _controlPanel.AddElement(_startSort);
        _controlPanel.AddElement(_stopSort);
        _controlPanel.AddElement(_step);
        _controlPanel.AddElement(_restart);
        
        _panel.AddElement(_status);
        _panel.AddElement(_info);
        _panel.AddElement(_canvas);
        _panel.AddElement(_controlPanel);
        
        _ui.AddElement(_panel);
        _ui.Run();
    }

    private void Step()
    {
        if(_isSorted) return;
        if (_i >= _j-1)
        {
            _array[_i].Item2 = true;
            _j = _i;
            _i = 0;
            _status.Text = $"Проход {60-_j} завершён. Элемент [{_array.Length - _i}] отсортирован.";
        };
        if (_j == 1)
        {
            _array[0].Item2 = true;
            _isSorted = true;
            _info.Text = "Сортировка завершена!";
            _status.Text = "Сортировка завершена!";
        }
        if (_array[_i].Item1 > _array[_i + 1].Item1)
        {
            (_array[_i].Item1, _array[_i+1].Item1) = (_array[_i+1].Item1, _array[_i].Item1);
            _status.Text = $"Меняем местами [{_i}]={_array[_i].Item1} <-> [{_i + 1}]={_array[_i + 1].Item1}";
        }
        _i++;
    }

    private void RenderCanvas()
    {
        for (int i = 0; i < _array.Length; i++)
        {
            Style style = _array[i].Item2 ? new Style(ConsoleColor.DarkGreen, ConsoleColor.Black) : Style.Default;
            _canvas.DrawLine(new Vec2I(i,_canvas.Height), new Vec2I(i,_array[i].Item1), '█', style);
            _canvas.DrawLine(new Vec2I(i,_array[i].Item1), new Vec2I(i,0), '█', new Style(ConsoleColor.Black, ConsoleColor.Black));
        }
        _canvas.DrawLine(new Vec2I(_i,_canvas.Height), new Vec2I(_i,_array[_i].Item1+1), '█', new Style(ConsoleColor.DarkRed, ConsoleColor.Black));
    }
}