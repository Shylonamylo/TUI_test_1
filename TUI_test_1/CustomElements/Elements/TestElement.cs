using TUI_Lib.Elements;

namespace TUI_test_1.CustomElements.Elements;

public class TestElement : TextElement
{
    public override string Text { get; set; } = "Привет мир! Я кастомный текстовый элемент созданный вне библиотеки!";
}