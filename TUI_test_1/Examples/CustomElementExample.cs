using TUI_Lib;
using TUI_Lib.Elements;
using TUI_test_1.CustomElements.Elements;

namespace TUI_test_1.Examples;

public class CustomElementExample
{
    private TUI _ui = new()
    {
        RequiredFPS = 100
    };
    
    StackPanel stackPanel = new();
    
    TestElement testElement = new();

    private TestElementWithoutRenderer testElementWithoutRenderer = new();
    
    public void Run()
    {
        testElementWithoutRenderer.MaxLength = 60;
        
        stackPanel.AddElement(testElement);
        stackPanel.AddElement(testElementWithoutRenderer);
        
        _ui.AddElement(stackPanel);
        _ui.Run();
    }
}