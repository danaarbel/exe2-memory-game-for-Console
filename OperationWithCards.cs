using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class OperationWithCards
    {
        public static Card RandomCardForComputer(Board i_Board)
        {
            int rowOfCard;
            char columnOfCard;
            Random random = new Random();
            rowOfCard = random.Next(1, i_Board.Row + 1);
            columnOfCard = (char)(random.Next(1, i_Board.Column + 1) + Constants.k_AsciiDifference);
            Card card = new Card(columnOfCard, rowOfCard);

            return card;
        }

        public static string ExtractColumn(string i_Input)
        {
            return i_Input.Substring(0, 1);
        }

        public static string ExtractRow(string i_Input)
        {
            return i_Input.Substring(1, 1);
        }

        public static int NumberOfHidenCards(Player i_PlayerNumberOne, Player i_PlayerNumberTwo, Board i_Board)
        {
            int totalPoints = i_PlayerNumberOne.Points + i_PlayerNumberTwo.Points;
            int totalPairs = i_Board.NumberOfCells / 2;

            return totalPairs - totalPoints;
        }

        public static bool CheckIfCardsAreMatch(Card i_CardNumberOne, Card i_CardNumberTwo, Board i_Board)
        {
            bool isMatch = false;
            string valueOfCardOne = i_Board.ValueInRealMatrix(i_CardNumberOne.Row, i_CardNumberOne.Column);
            string valueOfCardTwo = i_Board.ValueInRealMatrix(i_CardNumberTwo.Row, i_CardNumberTwo.Column);
            if (valueOfCardOne.Equals(valueOfCardTwo))
            {
                isMatch = true;
            }

            return isMatch;
        }
    }
}