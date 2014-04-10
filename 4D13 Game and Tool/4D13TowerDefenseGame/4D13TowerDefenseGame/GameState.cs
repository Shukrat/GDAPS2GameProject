using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4D13TowerDefenseGame
{
    class GameState
    {
        public int ChangeState(int gameState)
        {              
            // While the gameState does not equal close
            while (gameState != 5)
            {                
                while (gameState == 2) // while the game is running it will be in this while loop
                {
                    // game.Start(); // Plays the game
                    while (gameState == 4)
                    {
                        // game.Pause();
                        // game.PauseMenu();
                        // open up pause menu
                        // If the settings button is pressed/clicked                    
                        if (gameState == 3)
                        {
                            // game.SettingsMenu();
                            // opens settings window once it's created
                            return gameState;
                        }
                        // if the game is closed  from game pause menu
                        if (gameState == 5)
                        {
                            //game.Close();
                            return gameState;
                        }
                        else
                            return gameState;
                    }
                    return gameState;
                }
                if (gameState == 3) // if opened from title
                {
                    // game.SettingsMenu();
                    // opens settings window once it's created
                    return gameState;
                }
                // if the game is closed  from game title menu
                if (gameState == 5)
                {
                    //game.Close();
                    return gameState;
                }                
            }
            return gameState;
        }      

    }
}



