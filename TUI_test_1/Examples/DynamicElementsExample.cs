using TUI_Lib;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class DynamicElementsExample
{
    private readonly TUI _ui = new();

    public void Run()
    {
        Console.Clear();
        
        StackPanel mainPanel = new StackPanel();
        StackPanel listPanel = new StackPanel();

        TextBox input = new TextBox();
        Button addButton = new Button { Label = "Добавить элемент" };

        addButton.OnClick += () =>
        {
            if (!string.IsNullOrWhiteSpace(input.Text))
            {
                listPanel.AddElement(new TextBlock { Text = $"> {input.Text}" });
                input.Text = "";
            }
        };

        mainPanel.AddElement(input);
        mainPanel.AddElement(addButton);
        
        mainPanel.AddElement(listPanel);

        _ui.AddElement(mainPanel);
        _ui.Run();
    }
}