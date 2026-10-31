using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;

namespace TUI_test_1;

class Program
{
    private static readonly TUI _ui = new TUI()
    {
        RequiredFPS = 100
    };

    private static int _rightRacketPos = 15;
    private static int _leftRacketPos = 15;
    private static double _ballPosX = 15;
    private static double _ballPosY = 15;
    private static Vec2I _ballSpeed = new Vec2I(Random.Shared.Next(-4,3)+1, Random.Shared.Next(-4,3)+1);
    private static int _racketSize = 5 / 2;
    private static int _player1Score = 0;
    private static int _player2Score = 0;

    private static StackPanel _mainPanel = new StackPanel();

    private static UserCanvas _canvas = new UserCanvas(31, 31);

    private static StackPanel _scorePanel = new StackPanel()
    {
        Horizontal = true
    };

    private static TextBlock _fpsCounter = new TextBlock()
    {
        Text = _ui.FPS.ToString()
    };

    private static TextBlock _player1ScoreTextBlock = new TextBlock()
    {
        Text = $"Очки игрока 1: {_player1Score}",
    };

    static TextBlock _player2ScoreTextBlock = new TextBlock()
    {
        Text = $"Очки игрока 2: {_player2Score}",
    };
    
    static void Main(string[] args)
    {
        _ui.OnTick += (dt) =>
        {
            _fpsCounter.Text = _ui.FPS.ToString();

            Pong(dt);
        };

        _ui.OnKeyAvalible += (key) =>
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    _leftRacketPos -= _leftRacketPos>_racketSize+1?1:0;
                    break;
                case ConsoleKey.DownArrow:
                    _leftRacketPos += _leftRacketPos<30-(_racketSize+1)?1:0;
                    break;
                case ConsoleKey.W:
                    _rightRacketPos -= _rightRacketPos>_racketSize+1?1:0;
                    break;
                case ConsoleKey.S:
                    _rightRacketPos += _rightRacketPos<30-(_racketSize+1)?1:0;
                    break;
            }
        };
        
        _scorePanel.AddElement(_player1ScoreTextBlock);
        _scorePanel.AddElement(_player2ScoreTextBlock);
        
        _mainPanel.AddElement(_fpsCounter);
        _mainPanel.AddElement(_scorePanel);
        _mainPanel.AddElement(_canvas);
        
        _ui.AddElement(_mainPanel);
        
        _ui.Run();
    }
    
    private static void Pong(double dt)
    {
        _canvas.Clear();
            
        _canvas.DrawBox(0, 0,31,31,'@', '=', Style.Default);
            
        _canvas.DrawLine(new Vec2I(1, _rightRacketPos-_racketSize), new Vec2I(1, _rightRacketPos+_racketSize), '#', Style.Default);
        _canvas.DrawLine(new Vec2I(29, _leftRacketPos-_racketSize), new Vec2I(29, _leftRacketPos+_racketSize), '#', Style.Default);
            
        CalcCollision(dt);
            
        StepBall(dt);
            
        _canvas.SetCell((int)_ballPosX, (int)_ballPosY, '*', Style.Default);
    }

    private static void CalcCollision(double dt)
    {
        char collideChar = _canvas.GetCell((int)(_ballPosX + _ballSpeed.X * dt), (int)(_ballPosY + _ballSpeed.Y * dt)).Char;
            
        if (collideChar == '#')
        {
            _ballSpeed.X *= -1;
        }

        if (collideChar == '@')
        {
            _ballSpeed.Y *= -1;
        }
            
        if (collideChar == '=')
        {
            if (_ballPosX > 15)
            {
                _player1Score++;
                _player1ScoreTextBlock.Text = $"Очки игрока 1: {_player1Score}";
            }
            else
            {
                _player2Score++;
                _player2ScoreTextBlock.Text = $"Очки игрока 2: {_player2Score}";
            }

            ResetBall();
        }
    }

    private static void ResetBall()
    {
        _ballPosX = 15;
        _ballSpeed = new Vec2I(Random.Shared.Next(-4,3)+1, Random.Shared.Next(-4,3)+1);
    }

    private static void StepBall(double dt)
    {
        _ballPosX += _ballSpeed.X * dt;
        _ballPosY += _ballSpeed.Y * dt;
    }
}