using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SnakeGame.Models;

namespace SnakeGame.Services
{
	internal class GameEngine : IDisposable
	{
		private readonly object _sync = new object();
		private readonly System.Timers.Timer _timer;
		private GameState _state;

		public event EventHandler<GameStateEventArgs> StateChanged;
		public event EventHandler<GameOverEventArgs> GameOver;

		public GameState State
		{
			get
			{
				lock (_sync) { return _state; }
			}
		}

		public GameEngine(int columns, int rows, double interval = 200)
		{
			if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
			if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));
			if (interval <= 0) throw new ArgumentOutOfRangeException(nameof(interval));

			_state = new GameState(columns, rows);

			_timer = new Timer(interval)
			{
				AutoReset = true,
			};
			_timer.Elapsed += Timer_Elapsed;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			//su dung invoke de tranh loi cross-thread
			try
			{
				Tick();
			}
			catch(Exception)
			{
				//optional log
			}
		}

		//Bat dau tu dong click
		public void Start()
		{
			lock(_sync)
			{
				if(!_timer.Enabled)
				{
					_timer.Start();
				}
			}
		}

		//Dung tu dong click
		public void Stop()
		{
			lock(_sync)
			{
				if(_timer.Enabled)
				{
					_timer.Stop();
				}
			}
		}

		//Click 1 lan - huu dung cho unit test
		public bool Tick()
		{
			lock(_sync)
			{
				if (_state.IsGameOver) return false;

				var continued = _state.Tick();
				OnStateUpdate(_state);

				if(!_state.IsGameOver) return continued;

				OnGameOver(_state);
				return false;
			}
		}

		//Doi huong di chuyen cua ran
		public void ChangeDirection(Direction.Huong newDirection)
		{
			lock(_sync)
			{
				if(_state.IsGameOver) return;
				_state.ChangeDirection(newDirection);
			}
		}

		public void reset()
		{
			lock(_sync)
			{
				_state.Reset();
				OnStateUpdate(_state);
			}

		}

		protected virtual void OnStateUpdate(GameState state)
		{
			var handler = StateChanged;
			if(handler != null)
				handler(this, new GameStateEventArgs(state));
        }

		protected virtual void OnGameOver(GameState state)
		{
			var handler = GameOver;
			if(handler != null)
				handler(this, new GameOverEventArgs(state.Score, state));
        }

		public void Dispose()
		{
			lock(_sync)
			{
				if(_timer != null)
				{
					_timer.Elapsed -= Timer_Elapsed;
					_timer.Stop();
					_timer.Dispose();
				}
			}
		}

		//The loai su kien
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
