using TUI_Lib;
using TUI_test_1.CustomElements.Elements;

namespace TUI_test_1.Examples;

public class CustomElementExample
{
    private TUI _ui = new()
    {
        RequiredFPS = 100
    };
    
    TestElement testElement = new();
    
    public void Run()
    {  
        _ui.AddElement(testElement);
        _ui.Run();
    }
}