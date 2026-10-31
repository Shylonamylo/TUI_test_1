using TUI_Lib;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Elements;

namespace TUI_test_1.Examples;

public class PongExample
{
    private readonly TUI _ui = new() { RequiredFPS = 100 };

    private int _rightRacketPos = 15;
    private int _leftRacketPos = 15;
    
    private Vec2D _ballPos = new(15, 15);
    private Vec2D _ballSpeed;
    
    private int _racketSize = 5;
    private int _racketSizeHalf;
    private int _player1Score = 0;
    private int _player2Score = 0;

    private readonly StackPanel _mainPanel = new();
    private readonly UserCanvas _canvas = new(31, 31);
    private readonly StackPanel _scorePanel = new() { Horizontal = true };
    private readonly TextBlock _fpsCounter = new();
    private readonly TextBlock _smallTutor = new();
    private readonly TextBlock _player1ScoreTextBlock = new();
    private readonly TextBlock _player2ScoreTextBlock = new();

    public void Run()
    {
        Console.Clear();

        _racketSizeHalf = _racketSize / 2;
        
        ResetBall();

        _smallTutor.Text = "Левый игрок управляется W/S, Правый стрелками вверх/вниз";
        _smallTutor.MaxLength = _smallTutor.Text.Length; 
        _fpsCounter.Text = $"Текущий ФПС: {_ui.FPS.ToString()}";
        _player1ScoreTextBlock.Text = $"Очки игрока 1: {_player1Score}";
        _player2ScoreTextBlock.Text = $"Очки игрока 2: {_player2Score}";

        _ui.OnTick += (dt) =>
        {
            _fpsCounter.Text = $"Текущий ФПС: {_ui.FPS.ToString()}";
            Pong(dt);
        };

        _ui.OnKeyAvalible += (key) =>
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    _leftRacketPos -= _leftRacketPos > _racketSizeHalf + 1 ? 1 : 0;
                    break;
                case ConsoleKey.DownArrow:
                    _leftRacketPos += _leftRacketPos < 30 - (_racketSizeHalf + 1) ? 1 : 0;
                    break;
                case ConsoleKey.W:
                    _rightRacketPos -= _rightRacketPos > _racketSizeHalf + 1 ? 1 : 0;
                    break;
                case ConsoleKey.S:
                    _rightRacketPos += _rightRacketPos < 30 - (_racketSizeHalf + 1) ? 1 : 0;
                    break;
            }
        };

        _scorePanel.AddElement(_player1ScoreTextBlock);
        _scorePanel.AddElement(_player2ScoreTextBlock);

        _mainPanel.AddElement(_fpsCounter);
        _mainPanel.AddElement(_smallTutor);
        _mainPanel.AddElement(_scorePanel);
        _mainPanel.AddElement(_canvas);

        _ui.AddElement(_mainPanel);
        _ui.Run();
    }

    private void Pong(double dt)
    {
        _canvas.Clear();
        
        _canvas.DrawBox(0, 0, 31, 31, '@', '=', Style.Default);
        
        _canvas.DrawLine(new Vec2I(1, _rightRacketPos - _racketSizeHalf), new Vec2I(1, _rightRacketPos + _racketSizeHalf), '#', Style.Default);
        _canvas.DrawLine(new Vec2I(29, _leftRacketPos - _racketSizeHalf), new Vec2I(29, _leftRacketPos + _racketSizeHalf), '#', Style.Default);
        
        CalcCollision(dt);
        
        StepBall(dt);
        
        _canvas.SetCell((int)_ballPos.X, (int)_ballPos.Y, '*', Style.Default);
    }

    private void CalcCollision(double dt)
    {
        int nextX = (int)(_ballPos.X + _ballSpeed.X * dt);
        int nextY = (int)(_ballPos.Y + _ballSpeed.Y * dt);
        
        char collideChar = _canvas.GetCell(nextX, nextY).Char;
        
        if (collideChar == '#')
        {
            _ballSpeed.X = Math.Clamp(_ballSpeed.X * -1.25, -15.0, 15.0);
            _ballSpeed.Y = Math.Clamp(_ballSpeed.Y * 1.05, -15.0, 15.0);
        }

        if (collideChar == '@')
        {
            _ballSpeed.Y = Math.Clamp(_ballSpeed.Y * -1.0, -15.0, 15.0);
        }
            
        if (collideChar == '=')
        {
            if (_ballPos.X > 15)
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

    private void ResetBall()
    {
        _ballPos = new Vec2D(15, 15);
        
        double speedX = Random.Shared.Next(2, 5) * (Random.Shared.Next(2) == 0 ? 1 : -1);
        double speedY = Random.Shared.Next(2, 5) * (Random.Shared.Next(2) == 0 ? 1 : -1);
        
        _ballSpeed = new Vec2D(speedX, speedY);
    }

    private void StepBall(double dt)
    {
        _ballPos.X += _ballSpeed.X * dt;
        _ballPos.Y += _ballSpeed.Y * dt;
    }
}