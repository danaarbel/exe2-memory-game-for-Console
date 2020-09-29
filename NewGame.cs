using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class NewGame
    {
        public void MemoryGame()
        {
            Console.WriteLine("Hello! welcome to the memory game\nPlease enter your name");
            string nameOfPlayerOne = Console.ReadLine();
            Player playerNumberOne = new Player(nameOfPlayerOne);
            Player playerNumberTwo;
            Console.WriteLine("Hi {0}! if you playing with another player press 1 \nelse, if you want to play with The computer press 0", nameOfPlayerOne);
            string typeOfGame = Console.ReadLine();
            while (!InputValidation.ValidTypeGame(typeOfGame))
            {
                Console.WriteLine(ErrorMessages.SyntaxError);
                typeOfGame = Console.ReadLine();
            }

            if (typeOfGame.Equals(Constants.k_AnotherPlayer))
            {
                Console.WriteLine("Please enter the name of the second player");
                string nameOfPlayerTwo = Console.ReadLine();
                playerNumberTwo = new Player(nameOfPlayerTwo);
            }
            else
            {
                playerNumberTwo = new Player("Computer");
            }

            StartTheGame(playerNumberOne, playerNumberTwo);
            bool startAgain = true;
            while (startAgain)
            {
                Console.WriteLine("Do you want to start another game?\nPress Y for YES\nPress E to EXIT");
                string playAgain = Console.ReadLine();
                if (playAgain.Equals(Constants.k_Yes))
                {
                    Console.WriteLine("Let's start again!");
                    System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
                    playerNumberOne.Restart();
                    playerNumberTwo.Restart();
                    StartTheGame(playerNumberOne, playerNumberTwo);
                }
                else if (playAgain.Equals(Constants.k_Exit))
                {
                    Console.WriteLine("Bye bye! hope you enjoyed");
                    System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
                    startAgain = false;
                    EndGame.EndTheGame();
                }
                else
                {
                    Console.WriteLine(ErrorMessages.SyntaxError);
                }
            }
        }

        private void PrintResults(params object[] i_Objcts)
        {
            string resultsOfGame = string.Format(
                @"The game is over! 
The score of {0} is: {1}
The score of {2} is: {3}
The winner is: {4}!!",
                i_Objcts);
            System.Console.WriteLine(resultsOfGame);
        }

        private void StartTheGame(Player io_PlayerNumberOne, Player i_PlayerNumberTwo)
        {
            int rowsOfBoard = 0;
            int columnsOfBoard = 0;
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("Please enter the numbers of rows of the board\nPress ENTER\nAnd then enter the numbers of columns of the board");
                string inputRows = Console.ReadLine();
                string inputColumns = Console.ReadLine();
                if (InputValidation.InputIsNumber(inputRows) == Constants.k_NotNumber || InputValidation.InputIsNumber(inputColumns) == Constants.k_NotNumber)
                {
                    Console.WriteLine(ErrorMessages.SyntaxError);
                    continue;
                }

                rowsOfBoard = int.Parse(inputRows);
                columnsOfBoard = int.Parse(inputColumns);
                if (!InputValidation.SizeOfBoard(rowsOfBoard, columnsOfBoard))
                {
                    Console.WriteLine(ErrorMessages.RangeError);
                    continue;
                }

                correctInput = true;
            }

            Board board = new Board(rowsOfBoard, columnsOfBoard);
            io_PlayerNumberOne.Turn = true;
            board.ShowBoard();
            Console.WriteLine("{0} start the game", io_PlayerNumberOne.Name);
            System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
            PlayerMove(io_PlayerNumberOne, board, i_PlayerNumberTwo);
            while (OperationWithCards.NumberOfHidenCards(io_PlayerNumberOne, i_PlayerNumberTwo, board) != 0)
            {
                if (io_PlayerNumberOne.Turn)
                {
                    PlayerMove(io_PlayerNumberOne, board, i_PlayerNumberTwo);
                }
                else
                {
                    PlayerMove(i_PlayerNumberTwo, board, io_PlayerNumberOne);
                }
            }

            if (EndGame.IsTie(io_PlayerNumberOne, i_PlayerNumberTwo))
            {
                Console.WriteLine("There is a tie!\nThe score of both of you is: {0}", io_PlayerNumberOne.Points);
            }
            else
            {
                Player winner = EndGame.WinnerOfTheGame(io_PlayerNumberOne, i_PlayerNumberTwo);
                PrintResults(io_PlayerNumberOne.Name, io_PlayerNumberOne.Points, i_PlayerNumberTwo.Name, i_PlayerNumberTwo.Points, winner.Name);
            }
        }

        private void PlayerMove(Player io_Player, Board io_Board, Player io_NotPlayer)
        {
            Card cardNumberOne = ChooseValidCardInBoard(io_Player, io_Board);
            io_Board.FlipUpCardInBoard(cardNumberOne.Row, cardNumberOne.Column);
            io_Board.ShowBoard();
            Card cardNumberTwo = ChooseValidCardInBoard(io_Player, io_Board);
            io_Board.FlipUpCardInBoard(cardNumberTwo.Row, cardNumberTwo.Column);
            bool isMatch = OperationWithCards.CheckIfCardsAreMatch(cardNumberOne, cardNumberTwo, io_Board);
            if (isMatch)
            {
                io_Board.ShowBoard();
                io_Player.AddPointToPlayer();
            }
            else
            {
                io_Board.ShowBoard();
                Console.WriteLine("No match :(");
                System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
                io_Board.FlipDownCardInBoard(cardNumberOne.Row, cardNumberOne.Column);
                io_Board.FlipDownCardInBoard(cardNumberTwo.Row, cardNumberTwo.Column);
                io_Board.ShowBoard();
                io_Player.Turn = false;
                io_NotPlayer.Turn = true;
            }
        }

        private Card ChooseValidCardInBoard(Player i_Player, Board i_Board)
        {
            Card card;
            Console.WriteLine("It's {0} turn!", i_Player.Name);
            card = ChooseCard(i_Player, i_Board);
            bool valid = InputValidation.CheckIfEmptyCell(card, i_Board);
            while (valid == false)
            {
                if (!i_Player.Name.Equals("Computer"))
                {
                    Console.WriteLine("This card is already open!");
                }

                card = ChooseCard(i_Player, i_Board);
                valid = InputValidation.CheckIfEmptyCell(card, i_Board);
            }

            return card;
        }

        private Card ChooseCard(Player i_Player, Board i_Board)
        {
            Card card;
            if (i_Player.Name.Equals("Computer"))
            {
                System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
                card = OperationWithCards.RandomCardForComputer(i_Board);
            }
            else
            {
                bool correctInput = false;
                int rowOfCard = 0;
                char columnOfCard = ' ';
                while (!correctInput)
                {
                    Console.WriteLine("Please enter a cell on the board (or Q to QUIT)");
                    string input = Console.ReadLine();
                    if (EndGame.WantToQuit(input))
                    {
                        Console.WriteLine("Bye bye");
                        System.Threading.Thread.Sleep(Constants.k_MiliSeconds);
                        EndGame.EndTheGame();
                    }

                    bool correctLength = InputValidation.CorrectInputLength(input);
                    if (correctLength == false)
                    {
                        Console.WriteLine(ErrorMessages.LengthError);
                        continue;
                    }

                    string row = OperationWithCards.ExtractRow(input);
                    rowOfCard = InputValidation.InputIsNumber(row);
                    string column = OperationWithCards.ExtractColumn(input);
                    columnOfCard = InputValidation.InputIsValidChar(column);
                    if (rowOfCard == Constants.k_NotNumber || columnOfCard == Constants.k_No)
                    {
                        Console.WriteLine(ErrorMessages.SyntaxError);
                        continue;
                    }

                    bool validSyntaxRow = InputValidation.CorrectRowSyntax(rowOfCard, i_Board);
                    bool validSyntaxColumn = InputValidation.CorrectcolumnSyntax(columnOfCard, i_Board);
                    if (validSyntaxColumn == false || validSyntaxRow == false)
                    {
                        Console.WriteLine(ErrorMessages.RangeError);
                        continue;
                    }

                    correctInput = true;
                }

                card = new Card(columnOfCard, rowOfCard);
            }

            return card;
        }
    }
}