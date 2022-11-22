using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.Lib
{
    public class StringShortenNumber
    {
        public string text;

        public StringShortenNumber(int number)
        {
            if (number >= 1000000000)
                text = Math.Round((float)number / 1000000000, 2) + "G";
            else if (number >= 1000000)
                text = Math.Round((float)number / 1000000,2) + "M";
            else if (number >= 1000)
                text = Math.Round((float)number / 1000, 2) + "K";
            else
                text = number.ToString();
        }

        public string ToString()
        {
            return text;
        }
    }
}
