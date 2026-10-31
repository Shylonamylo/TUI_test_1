using System.Numerics;
using System.Text;
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

        StackPanel stackPanel1 = new StackPanel();

        TextBlock tb1 = new TextBox() { };
        
        var linesCount = tb1.GetLinesCount();
        
        stackPanel1.AddElement(tb1);
            
        ui.AddElement(stackPanel1);
        
        ui.Run();
    }
}