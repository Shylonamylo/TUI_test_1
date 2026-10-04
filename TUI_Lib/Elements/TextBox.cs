namespace TUI_Lib.Elements;

public class TextBox : TextBlock
{
    public event Action<string>? OnTextChanged;
    public event Action<ConsoleKey>? OnKeyDown;
    
    public override bool Selectable => true;

    private int _cursorPos = 0;
    
    public override bool HandleKey(ConsoleKeyInfo key)
    {
        bool handled = false;
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.DownArrow:
                break;            
            case ConsoleKey.LeftArrow:
                _cursorPos = _cursorPos>0?_cursorPos-1:0;
                handled = true;
                break;
            
            case ConsoleKey.RightArrow:
                _cursorPos = _cursorPos<Text.Length?_cursorPos+1:_cursorPos;
                handled = true;
                break;
            
            case ConsoleKey.Backspace:
                RemoveChar(_cursorPos);
                handled = true;
                break;
            case ConsoleKey.Enter:
                handled = true;
                break;

            default:
                InsertChar(key.KeyChar);
                handled = true;
                break;
        }

        if (handled)
        {
            OnKeyDown?.Invoke(key.Key);
        }
        
        return handled;
    }

    public char GetChar(int pos)
    {
        if (pos < 0 || pos >= Text.Length) return ' ';
        
        char character = Text[pos];
        return character;
    }

    private void InsertChar(char character)
    {
        try
        {
            List<char> _text = Text.ToList();
            _text.Insert(_cursorPos, character);
            Text = new string(_text.ToArray());
            _cursorPos++;
            OnTextChanged?.Invoke(Text);
        }
        catch
        {
            // ignored
        }
    }
    private void RemoveChar(int position)
    {
        try
        { 
            if (Text.Length > 0)
            {
                List<char> _text = Text.ToList();
                _text.RemoveAt(position-1);
                Text = new string(_text.ToArray());
                _cursorPos--;
                OnTextChanged?.Invoke(Text);
            }
        }
        catch
        {
            // ignored
        }
    }

    public int GetCursorPos()
    {
        return _cursorPos;
    }
}