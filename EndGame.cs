using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class EndGame
    {
        public static bool WantToQuit(string i_Input)
        {
            bool quit = false;
            if (i_Input.Equals(Constants.k_Quit))
            {
                quit = true;
            }

            return quit;
        }

        public static void EndTheGame()
        {
            Environment.Exit(0);
        }

        public static Player WinnerOfTheGame(Player i_PlayerNumberOne, Player i_PlayerNumberTwo)
        {
            Player winner = i_PlayerNumberTwo;
            if (i_PlayerNumberOne.Points > i_PlayerNumberTwo.Points)
            {
                winner = i_PlayerNumberOne;
            }

            return winner;
        }

        public static bool IsTie(Player i_PlayerNumberOne, Player i_PlayerNumberTwo)
        {
            return i_PlayerNumberOne.Points == i_PlayerNumberTwo.Points;
        }
    }
}