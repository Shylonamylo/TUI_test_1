using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class BasicInterfaceExample
{
    private readonly TUI _ui = new();

    public void Run()
    {
        Console.Clear();
        
        StackPanel mainPanel = new StackPanel();

        TextBlock title = new TextBlock
        {
            Text = "Добро пожаловать в TUI!",
            MaxLength = 50
        };
        
        TextBox input = new TextBox();
        
        Button submitButton = new Button
        {
            Label = "Нажми меня!"
        };
        
        TextBlock result = new TextBlock
        {
            Text = "Здесь появится ваш текст после нажатия на кнопку!",
            MaxLength = 50
        };

        submitButton.OnClick += () =>
        {
            result.Text = $"Вы ввели: {input.Text}";
        };

        mainPanel.AddElement(title);
        mainPanel.AddElement(input);
        mainPanel.AddElement(submitButton);
        mainPanel.AddElement(result);

        _ui.AddElement(mainPanel);
        _ui.Run();
    }
}