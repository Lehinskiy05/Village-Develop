using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Controller
{
    public class Controller
    {
        private Form Form;
        private Model.Model model;
        public double FPS;
        public double PlayerSpeed;

        public Controller(Form form, Model.Model model)
        {
            Form = form;
            FPS = 60;
            PlayerSpeed = 0.4;

            MainCycle();
        }

        private void MainCycle()
        {
            
        }
    }
}
