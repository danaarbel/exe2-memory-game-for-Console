﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class Board
    {
        private readonly int m_Rows = 0;
        private readonly int m_Columns = 0;
        private readonly int m_NumberOfCells = 0;
        private readonly string[,] m_RealMatrix;
        private string[,] m_HideMatrix;

        public Board(int i_Rows, int i_Columns)
        {
            m_Rows = i_Rows;
            m_Columns = i_Columns;
            m_NumberOfCells = i_Rows * i_Columns;
            m_RealMatrix = new string[i_Rows, i_Columns];
            m_HideMatrix = new string[i_Rows, i_Columns];
            SetRealMatrix();
            SetHideMatrix();
        }

        private void SetRealMatrix()
        {
            Random random = new Random();
            List<string> abc = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int numberOfPairs = m_NumberOfCells / 2;
            while (numberOfPairs != 0)
            {
                string letterCell = abc[random.Next(abc.Count)];
                int locationCellCard1x = random.Next(m_Rows);
                int locationCellCard1y = random.Next(m_Columns);
                int locationCellCard2x = random.Next(m_Rows);
                int locationCellCard2y = random.Next(m_Columns);
                while (locationCellCard1x == locationCellCard2x && locationCellCard1y == locationCellCard2y)
                {
                    locationCellCard2x = random.Next(m_Rows);
                    locationCellCard2y = random.Next(m_Columns);
                }

                if (m_RealMatrix[locationCellCard1x, locationCellCard1y] != null || m_RealMatrix[locationCellCard2x, locationCellCard2y] != null)
                {
                    continue;
                }
                else
                {
                    m_RealMatrix[locationCellCard1x, locationCellCard1y] = letterCell;
                    m_RealMatrix[locationCellCard2x, locationCellCard2y] = letterCell;
                    abc.Remove(letterCell);
                    numberOfPairs--;
                }
            }
        }

        private void SetHideMatrix()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Columns; j++)
                {
                    m_HideMatrix[i, j] = " ";
                }
            }
        }

        public void ShowBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            StringBuilder board = new StringBuilder();
            List<string> indexColumn = new List<string>() { "A", "B", "C", "D", "E", "F" };
            List<string> indexRow = new List<string>() { "1", "2", "3", "4", "5", "6" };

            board.Append("   ");
            for (int j = 0; j < m_Columns; j++)
            {
                board.Append(" " + indexColumn[j] + "  ");
            }

            for (int i = 0; i < m_Rows; i++)
            {
                board.Append(DividingLine());
                board.Append("\n");
                board.Append(indexRow[i] + " |");
                for (int s = 0; s < m_Columns; s++)
                {
                    board.Append(" " + m_HideMatrix[i, s] + " |");
                }
            }

            board.Append(DividingLine());
            Console.WriteLine(board.ToString());
        }

        private string DividingLine()
        {
            StringBuilder boarder = new StringBuilder();
            boarder.Append("\n");
            boarder.Append("  ");
            for (int k = 0; k < this.m_Columns; k++)
            {
                boarder.Append("====");
            }

            boarder.Append("=");
            return boarder.ToString();
        }

        public void FlipUpCardInBoard(int i_Row, int i_Column)
        {
            m_HideMatrix[i_Row - 1, i_Column - 1] = m_RealMatrix[i_Row - 1, i_Column - 1];
        }

        public void FlipDownCardInBoard(int i_Row, int i_Column)
        {
            m_HideMatrix[i_Row - 1, i_Column - 1] = " ";
        }

        public int Row
        {
            get { return m_Rows; }
        }

        public int Column
        {
            get { return m_Columns; }
        }

        public bool IsEmptyCell(int i_Row, int i_Column)
        {
            bool valid = false;
            if (m_HideMatrix[i_Row - 1, i_Column - 1] == " ")
            {
                valid = true;
            }

            return valid;
        }

        public int NumberOfCells
        {
            get { return m_NumberOfCells; }
        }

        public string ValueInRealMatrix(int i_Row, int i_Column)
        {
            return m_RealMatrix[i_Row - 1, i_Column - 1];
        }
    }
}