using System.Drawing;
using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class RendererStressTest
{
    private readonly TUI _ui = new() { RequiredFPS = 100000 };

    private readonly StackPanel _stackPanel = new();
    
    private readonly UserCanvas _userCanvas = new(50, 50);
    
    private readonly TextBlock _fpsCounter = new();
    
    public void Run()
    {
        _ui.OnTick += (dt) =>
        {
            _fpsCounter.Text = $"{_ui.FPS} FPS";
            _userCanvas.Clear();
            for (int y = 0; y < _userCanvas.Height; y++)
            {
                for (int x = 0; x < _userCanvas.Width; x++)
                {
                    _userCanvas.SetCell(x,y,'@',new Style(Color.FromArgb(255,Random.Shared.Next(0,256),Random.Shared.Next(0,256),Random.Shared.Next(0,256)),Color.FromArgb(255,Random.Shared.Next(0,256),Random.Shared.Next(0,256),Random.Shared.Next(0,256))));
                }
            }
        };
        
        _stackPanel.AddElement(_fpsCounter);
        _stackPanel.AddElement(_userCanvas);
        
        _ui.AddElement(_stackPanel);
        _ui.Run();
    }
}