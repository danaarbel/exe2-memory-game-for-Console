using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class Card
    {
        private readonly int m_Row;
        private readonly char m_Column;

        public Card(char i_ColumnOfCard, int i_RowOfCard)
        {
            m_Row = i_RowOfCard;
            m_Column = i_ColumnOfCard;
        }

        public int Row
        {
            get { return m_Row; }
        }

        public int Column
        {
            get { return m_Column - Constants.k_AsciiDifference; }
        }
    }
}