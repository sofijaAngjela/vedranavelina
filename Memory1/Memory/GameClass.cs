using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memory
{
    class GameClass
    {
        Square[] all = new Square[17];
        List<Square> listSelected;
        List<Square> listGuessed;

        public GameClass()
        {

        }

        Random random = new Random();

        public Square[] shuffle(Square[] allArray)
        {

            for (int i = allArray.Length - 1; i >= 1; i--)
            {
                int j = random.Next(0, i);
                swap(allArray[j], allArray[i]);
            }

            return allArray;
        }

        public void swap(Square x, Square y)
        {

            Square z = null;
            z = x;
            x = y;
            y = z;
        }
    }
}
