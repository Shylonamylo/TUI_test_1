using TUI_Lib.Elements;

namespace TUI_test_1.CustomElements.Elements;

public class TestElementWithoutRenderer : TextBlock
{
    public override string Text { get; set; } = "Привет мир! Я кастомный текстовый элемент созданный вне библиотеки и у меня нет своего отрисовщика, я использую отрисовщик базового класса!";
}