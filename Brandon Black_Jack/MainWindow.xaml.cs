using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brandon_Black_Jack
{
    /// <summary>

    /// </summary>
    /// <summary>

    /// </summary>
    public partial class MainWindow : Window
    {
        private short pScore, dScore;
        private List<short> pCards, dCards;
        private List<Image> pField, dField;
        private List<Deck> Decks;
        private int bet, nextBet, money, round, deckNum;
        private Card dTwo;
        private bool pAce, dAce;
        public MainWindow()
        {
            InitializeComponent();
            round = 0;
            RoundCount.Text = round.ToString();
            money = 500;
            MoneyAmount.Text = money.ToString();
            bet = 1;
            nextBet = 1;
            BetAmount.Text = bet.ToString();
            NextBetAmount.Text = bet.ToString();
            deckNum = 1;
            DeckCount.Text = deckNum.ToString();
            Decks = new List<Deck>();
            makeDecks();
            pCards = new List<short>();
            dCards = new List<short>();
            pField = new List<Image>();
            dField = new List<Image>();
            makeField();
            roundSetup();

        }
    
        private void standClick(object sender, RoutedEventArgs e)
        {
            dealerCardImage2.Source = makeImage(dTwo.image);
            dScore = (short)dCards.Sum(x => Convert.ToInt32(x));
            if (pAce && (pScore + 10) < 22)
            {
                pScore += 10;
            }
            while (dScore <= pScore && dScore < 17)
            {
                if (dAce && ((dScore + 10) > 16 || (dScore + 10) > pScore))
                {
                    System.Diagnostics.Debug.WriteLine(dScore.ToString());
                    break;
                }
                else
                {
                    Card dNewCard = takeCard();
                    dCards.Add(dNewCard.value);
                    dScore = (short)dCards.Sum(x => Convert.ToInt32(x));
                    dField[dCards.Count - 1].Source = makeImage(dNewCard.image);
                }
            }
            if (dAce && (dScore + 10) < 22)
            {
                dScore += 10;
            }
            if (pScore > dScore)
            {
                Notifications.Text = "You Win!";
                money += 2 * bet;
                MoneyAmount.Text = money.ToString();
                standButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                nextButton.IsEnabled = true;

            }
            else if (pScore < dScore)
            {
                Notifications.Text = "You Lose";
                standButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                nextButton.IsEnabled = true;
                if (money == 0)
                {
                    Notifications.Text = "Game Over";
                    nextButton.IsEnabled = false;
                }
            }
            else
            {
                Notifications.Text = "Tied Round";
                money += bet;
                MoneyAmount.Text = money.ToString();
                standButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                nextButton.IsEnabled = true;
            }
        }

        private void hitClick(object sender, RoutedEventArgs e)
        {

            if (pCards.Count < 21)
            {
                Card newCard = takeCard();
                pCards.Add(newCard.value);
                pScore = (short)pCards.Sum(x => Convert.ToInt32(x));
                DoubleBut.IsEnabled = false;
                if (pAce)
                {
                    ScoreCount.Text = pScore.ToString() + " or " + (pScore + 10).ToString();
                }
                else
                {
                    ScoreCount.Text = pScore.ToString();
                }
                pField[pCards.Count - 1].Source = makeImage(newCard.image);
                if (pScore > 21)
                {
                    Notifications.Text = "You Lose";
                    standButton.IsEnabled = false;
                    hitButton.IsEnabled = false;
                    nextButton.IsEnabled = true;
                    if (money == 0)
                    {
                        Notifications.Text = "Game Over";
                        nextButton.IsEnabled = false;
                    }
                }
            }
        }

        private void DoubleClick(object sender, RoutedEventArgs e)
        {
            if (pCards.Count < 21)
            {
                Card newCard = takeCard();
                pCards.Add(newCard.value);
                pScore = (short)pCards.Sum(x => Convert.ToInt32(x));
                bet = nextBet * 2;
                BetAmount.Text = bet.ToString();
                DoubleBut.IsEnabled = false;
                if (pAce)
                {
                    ScoreCount.Text = pScore.ToString() + " or " + (pScore + 10).ToString();
                }
                else
                {
                    ScoreCount.Text = pScore.ToString();
                }
                pField[pCards.Count - 1].Source = makeImage(newCard.image);
                dealerCardImage2.Source = makeImage(dTwo.image);
                dScore = (short)dCards.Sum(x => Convert.ToInt32(x));
                if (pAce && (pScore + 10) < 22)
                {
                    pScore += 10;
                }
                while (dScore <= pScore && dScore < 17)
                {
                    if (dAce && ((dScore + 10) > 16 || (dScore + 10) > pScore))
                    {
                        System.Diagnostics.Debug.WriteLine(dScore.ToString());
                        break;
                    }
                    else
                    {
                        Card dNewCard = takeCard();
                        dCards.Add(dNewCard.value);
                        dScore = (short)dCards.Sum(x => Convert.ToInt32(x));
                        dField[dCards.Count - 1].Source = makeImage(dNewCard.image);
                    }
                }
                if (dAce && (dScore + 10) < 22)
                {
                    dScore += 10;
                }
                if (pScore > dScore)
                {
                    Notifications.Text = "You Win!";
                    money += 2 * bet;
                    MoneyAmount.Text = money.ToString();
                    standButton.IsEnabled = false;
                    hitButton.IsEnabled = false;
                    nextButton.IsEnabled = true;

                }
                else if (pScore < dScore)
                {
                    Notifications.Text = "You Lose";
                    standButton.IsEnabled = false;
                    hitButton.IsEnabled = false;
                    nextButton.IsEnabled = true;
                    if (money == 0)
                    {
                        Notifications.Text = "Game Over";
                        nextButton.IsEnabled = false;
                    }
                }
                else
                {
                    Notifications.Text = "Tied Round";
                    money += bet;
                    MoneyAmount.Text = money.ToString();
                    standButton.IsEnabled = false;
                    hitButton.IsEnabled = false;
                    nextButton.IsEnabled = true;
                }
            }
        }

        private void nextClick(object sender, RoutedEventArgs e)
        {
            roundSetup();
            DoubleBut.IsEnabled = true;
        }


        public void roundSetup()
        {
            for (int c = 0; c < pCards.Count; c++)
            {
                pField[c].Source = makeImage("Card-backs-grid-blue.png");
            }
            for (int d = 0; d < dCards.Count; d++)
            {
                dField[d].Source = makeImage("Card-backs-grid-red.png");
            }
            pCards.Clear();
            dCards.Clear();
            for (int x = 0; x < 8; x++)
            {
                Decks[x].ResetDeck();
            }

            bet = nextBet;
            if (bet > money) { bet = money; }
            BetAmount.Text = bet.ToString();
            BetScrollBar.Maximum = money;
            money -= bet;
            MoneyAmount.Text = money.ToString();
            pAce = false;
            dAce = false;
            standButton.IsEnabled = true;
            hitButton.IsEnabled = true;
            nextButton.IsEnabled = false;

            round++;
            RoundCount.Text = round.ToString();
            Notifications.Text = "Round " + round.ToString();
            Card pOne = takeCard();
            if (pOne.value == 1) { pAce = true; }
            pCards.Add(pOne.value);
            Card pTwo = takeCard();
            if (pTwo.value == 1) { pAce = true; }
            pCards.Add(pTwo.value);
            playerCardImage1.Source = makeImage(pOne.image);
            playerCardImage2.Source = makeImage(pTwo.image);
            pScore = (short)pCards.Sum(x => Convert.ToInt32(x));
            if (pAce)
            {
                ScoreCount.Text = pScore.ToString() + " or " + (pScore + 10).ToString();
            }
            else
            {
                ScoreCount.Text = pScore.ToString();
            }
            Card dOne = takeCard();
            if (dOne.value == 1) { dAce = true; }
            dCards.Add(dOne.value);
            dTwo = takeCard();
            if (dTwo.value == 1) { dAce = true; }
            dCards.Add(dTwo.value);
            dealerCardImage1.Source = makeImage(dOne.image);
            if (pAce && pCards.Contains(10))
            {
                Notifications.Text = "Blackjack!";
                money += (bet + (3 / 2) * bet);
                MoneyAmount.Text = money.ToString();
                standButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                nextButton.IsEnabled = true;
            }
            else if (dAce && dCards.Contains(10))
            {
                dealerCardImage2.Source = makeImage(dTwo.image);
                Notifications.Text = "You Lose";
                standButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                nextButton.IsEnabled = true;
                if (money == 0)
                {
                    Notifications.Text = "Game Over";
                    nextButton.IsEnabled = false;
                }
            }
        }

        private void DeckScroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            deckNum = (int)DeckScrollBar.Value;
            DeckCount.Text = DeckScrollBar.Value.ToString();
        }

        private void BetScroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            nextBet = (int)BetScrollBar.Value;
            NextBetAmount.Text = BetScrollBar.Value.ToString();
        }

        public BitmapImage makeImage(string imgFile)
        {       
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("pack://application:,,,/cards/" + imgFile);
                image.EndInit();
            return image;
        }
        public void makeField()
        {
            pField.Add(playerCardImage1);
            pField.Add(playerCardImage2);
            pField.Add(playerCardImage3);
            pField.Add(playerCardImage4);
            pField.Add(playerCardImage5);
            pField.Add(playerCardImage6);
            pField.Add(playerCardImage7);
            pField.Add(playerCardImage8);
            pField.Add(playerCardImage9);
            pField.Add(playerCardImage10);
            pField.Add(playerCardImage11);
            pField.Add(playerCardImage12);
    
            dField.Add(dealerCardImage1);
            dField.Add(dealerCardImage2);
            dField.Add(dealerCardImage3);
            dField.Add(dealerCardImage4);
            dField.Add(dealerCardImage5);
            dField.Add(dealerCardImage6);
            dField.Add(dealerCardImage7);
            dField.Add(dealerCardImage8);
            dField.Add(dealerCardImage9);
            dField.Add(dealerCardImage10);
            dField.Add(dealerCardImage11);
            dField.Add(dealerCardImage12);

        }

        public void makeDecks()
        {
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
            Decks.Add(new Deck());
        }


        private Card takeCard()
        {
            Random rnd = new Random();
            int r = rnd.Next(deckNum);
            Deck pick = Decks[r];
            return pick.PickACard();
        }
    }
}
