using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Start_Splash_ScreenGame_Screen
{
    public partial class splashScreen : Form
    {

        public splashScreen()
        {
            InitializeComponent();
        }

        private void Exit_MouseClick(object sender, MouseEventArgs e) // pressing exit
        {
            splashScreen.ActiveForm.Close(); // exits the start menu
        }

        private void Start_Click(object sender, EventArgs e) // pressing start
        {
            GameScreen game = new GameScreen(); // creates new gamescreen object
            game.Show(); // shows game screen
            
        }

        private void Settings_Click(object sender, EventArgs e) // pressing settings button
        {
            Settings sets = new Settings(); // create new settings object
            sets.Show(); // show settings menu
        }
    }
}
