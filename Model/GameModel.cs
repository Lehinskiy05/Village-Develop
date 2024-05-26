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
        public readonly List<Guest> Guests;
        public int MaxGuestsCount;
        private GameForm gameForm;
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


        public GameModel(GameForm gameForm)
        {
            this.gameForm = gameForm;
            Map = new Map(gameForm, this);
            Player = new Player(this, this.gameForm);
            Guests = new List<Guest>();
            _gameState = GameState.Game;
            MaxGuestsCount = 3;

            Guests.Add(new Guest(gameForm, this, 1));
        }

        public void Update(int delta)
        {
            foreach (var guest in Guests)
            {
                guest.Update(delta);
                gameForm.UpdateControls();
            }
        }
    }
}
