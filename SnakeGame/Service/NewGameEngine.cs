using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SnakeGame.Models;

namespace SnakeGame.Services
{
    internal class NewGameEngine
    {

        private GameState _state;

        public event EventHandler<GameStateEventArgs> StateChanged;
        public event EventHandler<GameOverEventArgs> GameOver;

        public GameState State
        {
            get
            {
                return _state;
            }
        }

        public NewGameEngine(int columns, int rows, double interval = 200)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));

            _state = new GameState(columns, rows);
        }

        

        // Bắt đầu tự động click
        

        // Click 1 lần - hữu dụng cho unit test
        public bool Tick()
        {
            if (_state.IsGameOver) return false;

            var continued = _state.Tick();
            OnStateUpdate(_state);

            if (!_state.IsGameOver) return continued;

            OnGameOver(_state);
            return false;
        }

        // Đổi hướng di chuyển của rắn
        public void ChangeDirection(Direction.Huong newDirection)
        {
            if (_state.IsGameOver) return;
            _state.ChangeDirection(newDirection);
        }

        public void reset()
        {
            _state.Reset();
            OnStateUpdate(_state);

        }

        protected virtual void OnStateUpdate(GameState state)
        {
            var handler = StateChanged;
            if (handler != null)
                handler(this, new GameStateEventArgs(state));
        }

        protected virtual void OnGameOver(GameState state)
        {
            var handler = GameOver;
            if (handler != null)
                handler(this, new GameOverEventArgs(state.Score, state));
        }

        

        // Thể loại sự kiện
        public sealed class GameStateEventArgs : EventArgs
        {
            public GameState State { get; private set; }
            public GameStateEventArgs(GameState state)
            {
                State = state ?? throw new ArgumentNullException(nameof(state));
            }
        }

        public sealed class GameOverEventArgs : EventArgs
        {
            public int Score { get; private set; }
            public GameState State { get; private set; }
            public GameOverEventArgs(int score, GameState state) { Score = score; State = state ?? throw new ArgumentNullException(nameof(state)); }
        }
    }
}
