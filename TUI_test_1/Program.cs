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

        StackPanel mainPanel = new StackPanel();
        
        Button addItemButton = new Button()
        {
            Label = "Добавить",
        };
        
        StackPanel addInfoNamePanel = new StackPanel()
        {
            Horizontal = true
        };

        TextBlock fpsCounter = new TextBlock()
        {
            Text = ui.FPS.ToString()
        };
        
        TextBlock addInfoNameLabel = new TextBlock()
        {
            Text = "Введите название"
        };
        
        TextBox addInfoNameText = new TextBox();
        
        StackPanel itemsPanel = new StackPanel();

        addItemButton.OnClick += () =>
        {
            itemsPanel.AddElement(new TextBlock(){Text = addInfoNameText.Text});
        };

        ui.OnTick += (dt) =>
        {
            fpsCounter.Text = ui.FPS.ToString();
        };

        addInfoNamePanel.AddElement(addInfoNameLabel);
        addInfoNamePanel.AddElement(addInfoNameText);
        
        mainPanel.AddElement(fpsCounter);
        mainPanel.AddElement(addInfoNamePanel);
        mainPanel.AddElement(addItemButton);
        mainPanel.AddElement(itemsPanel);
        
        ui.AddElement(mainPanel);
        
        ui.Run();
    }
}