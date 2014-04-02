using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseGamePlay
{
    class GameState
    {
        int gameState;
        string titleText;

        public void ChangeState()
        {
              
            // While it's in the game
            while (gameState == 2)
            {
                // if the game is paused
                if (gameState == 4)
                {
                    game.Pause();
                    // If the menu button is pressed/clicked
                    if (gameState == 3)
                    {
                        game.OpenMenu();
                    }

                    // if the game is closed directly from game
                    if (gameState == 5)
                    {
                        game.Close();
                    }
                }
            }
        }

        public void startGame()
        {            
             gameState = 1; // title
             Console.WriteLine("Insert title here."); // output title text
            if (/* start button clicked */) 
            {
                gameState =2;// chagne gamestate to 2 and start
            }
            if (/* menu button clicked */) 
            {
                gameState =3;// chagne gamestate to 3 and open menu
            }
            if (/* close button clicked */) 
            {
                gameState =5;// chagne gamestate to 5 and close game
            }
        }


    }
}



