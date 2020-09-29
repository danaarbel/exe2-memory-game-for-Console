using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    public class ErrorMessages
    {
        private static string s_SyntaxError = "The input has worng syntax!";
        private static string s_RangeError = "The input is not in the board range!";
        private static string s_LengthError = "The Length of input is not valid!";

        public static string SyntaxError
        {
            get { return s_SyntaxError; }
        }

        public static string RangeError
        {
            get { return s_RangeError; }
        }

        public static string LengthError
        {
            get { return s_LengthError; }
        }
    }
}