using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public class GameModel
    {
        public readonly Map Map;
        public readonly Player Player;
        public readonly List<Customer> Customers;
        private GameState _gameState;

        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<GameState> GameStateChanged;

        public event Action LeftKeyDown;
        public event Action RightKeyDown;
        public event Action UpKeyDown;
        public event Action DownKeyDown;
        public event Action EKeyDown;
        public event Action QKeyDown;

        public GameModel()
        {
            Map = new Map();
            Player = new Player(this, new Point(350, 430), new Size(25, 40));
            Customers = new List<Customer>();
            _gameState = GameState.Game;
        }

        public void CloseGame()
        {
            throw new NotImplementedException();
        }

        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                case Keys.Left:
                    LeftKeyDown?.Invoke();
                    break;
                case Keys.D:
                case Keys.Right:
                    RightKeyDown?.Invoke();
                    break;
                case Keys.W:
                case Keys.Up:
                    UpKeyDown?.Invoke();
                    break;
                case Keys.S:
                case Keys.Down:
                    DownKeyDown?.Invoke();
                    break;
                case Keys.E:
                    EKeyDown?.Invoke();
                    break;
                case Keys.Q:
                    QKeyDown?.Invoke();
                    break;
            }
        }
    }
}
