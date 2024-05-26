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
        private GameForm gameForm;


        public GameModel(GameForm gameForm)
        {
            this.gameForm = gameForm;
            Map = new Map(gameForm, this);
            Player = new Player(this, this.gameForm);
            Guests = new List<Guest>();
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
