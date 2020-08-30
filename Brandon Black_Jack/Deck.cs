using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandon_Black_Jack
{
    class Deck
    {
        
        List<Card> cardList;

        public Deck()
        {
            BuildDeck();
        }

        public void BuildDeck()
        {
            cardList = new List<Card>();
            string[] suits = { "clubs", "diamonds", "hearts", "spades" };

            short V, suit = 0;
            string I;
            for (short x = 1; x < 14; x++)
            {
                if (x == 1)
                {
                    V = x;
                    I = suits[suit] + "/Ace-of-" + suits[suit] + ".png";
                }
                else if (x > 10)
                {
                    if (x == 11)
                    {
                        I = suits[suit] + "/Jack-of-" + suits[suit] + ".png";
                    }
                    else if (x == 12)
                    {
                        I = suits[suit] + "/King-of-" + suits[suit] + ".png";
                    }
                    else
                    {
                        I = suits[suit] + "/Queen-of-" + suits[suit] + ".png";
                        if (suit < 3)
                        {
                            suit++;
                            x = 0;
                        }
                    }
                    V = 10;
                }
                else
                {
                    V = x;
                    I = suits[suit] + "/" + V.ToString() + "-of-" + suits[suit] + ".png";
                }
                cardList.Add(new Card { value = V, image = I });
            }
        }

        public void ResetDeck()
        {
            cardList.Clear();
            BuildDeck();
        }

        public Card PickACard()
        {
            Random rnd = new Random();
            int r = rnd.Next(cardList.Count);
            Card pick = cardList[r];
            cardList.Remove(cardList[r]);
            return pick;
        }
    }
}
