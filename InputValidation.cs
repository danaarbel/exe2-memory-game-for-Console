using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class InputValidation
    {
        public static bool CheckIfEmptyCell(Card i_Card, Board i_Board)
        {
            bool isEmpty = i_Board.IsEmptyCell(i_Card.Row, i_Card.Column);

            return isEmpty;
        }

        public static bool CorrectRowSyntax(int i_RowOfCard, Board i_Board)
        {
            bool correctInput = false;
            if (i_RowOfCard <= i_Board.Row && i_RowOfCard >= Constants.k_MinimumRowOrColumn)
            {
                correctInput = true;
            }

            return correctInput;
        }

        public static bool CorrectcolumnSyntax(char i_ColumnOfCard, Board i_Board)
        {
            bool correctInput = false;
            if (i_ColumnOfCard - Constants.k_AsciiDifference <= i_Board.Column && i_ColumnOfCard - Constants.k_AsciiDifference >= Constants.k_MinimumRowOrColumn)
            {
                correctInput = true;
            }

            return correctInput;
        }

        public static int InputIsNumber(string i_Input)
        {
            int valid = Constants.k_NotNumber;
            int inputNumber;
            bool isNumber = int.TryParse(i_Input, out inputNumber);
            if (isNumber)
            {
                valid = inputNumber;
            }

            return valid;
        }

        public static char InputIsValidChar(string i_Input)
        {
            char valid = Constants.k_No;
            char inputChar;
            bool IsChar = char.TryParse(i_Input, out inputChar);

            if (IsChar && inputChar >= 'A' && inputChar <= 'Z')
            {
                valid = inputChar;
            }

            return valid;
        }

        public static bool CorrectInputLength(string i_Input)
        {
            return i_Input.Length == Constants.k_LengthOfCard;
        }

        public static bool ValidTypeGame(string i_TypeOfGame)
        {
            bool valid = false;
            if (i_TypeOfGame.Equals(Constants.k_CompterPlayer) || i_TypeOfGame.Equals(Constants.k_AnotherPlayer))
            {
                valid = true;
            }

            return valid;
        }

        public static bool SizeOfBoard(int i_RowsOfBoard, int i_ColumnsOfBoard)
        {
            bool valid = false;

            if ((i_RowsOfBoard * i_ColumnsOfBoard) % 2 == 0)
            {
                if (i_RowsOfBoard <= Constants.k_MaxsimumSizeOfBoard && i_RowsOfBoard >= Constants.k_MinimumSizeOfBoard && i_ColumnsOfBoard <= Constants.k_MaxsimumSizeOfBoard && i_ColumnsOfBoard >= Constants.k_MinimumSizeOfBoard)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}