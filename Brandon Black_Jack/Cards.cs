using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandon_Black_Jack
{
    class Card
    {

        public short value;
        public string image;

        public Card()
        {
            value = -1;
            image = "unknown";
        }

        public Card(short num, string anImage)
        {
            value = num;
            image = anImage;
        }

    }
}
