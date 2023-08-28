using System;
using System.Threading;
using static BlackJack.Program;

namespace BlackJack
{
    internal class Program
    {
        public class Card
        {
            public string cardSuit;
            public int cardValue;
            public string cardName;

            public Card(int value, string suit)
            {
                cardSuit = suit;
                cardValue = value;
                cardName = value.ToString() + " of " + cardSuit;
            }
            public Card(string value, string suit)
            {
                cardSuit = suit;
                if (value == "A")
                {
                    cardValue = 11;
                }
                else
                {
                    cardValue = 10;
                }
                cardName = value + " of " + cardSuit;
            }
        }

        public class Deck
        {
            public List<Card> cards;

            public Deck()
            {
                cards = new List<Card>();
            }

            public static List<Card> LoadDeck(List<Card> _deck, string suit)
            {
                List<Card> deck = _deck;
                for (int i = 2; i <= 10; i++)
                {
                    deck.Add(new Card(i, suit));
                }
                deck.Add(new Card("J", suit));
                deck.Add(new Card("Q", suit));
                deck.Add(new Card("K", suit));
                deck.Add(new Card("A", suit));
                return deck;
            }
        }

        public abstract class Hand
        {
            public List<Card> handCards;
            public virtual Card DrawCard(ref List<Card> deck)
            {
                int cardIndex = new Random().Next(deck.Count);
                Card card = deck[cardIndex];
                deck.Remove(deck[cardIndex]);
                Console.WriteLine($"뽑은 카드는 {card.cardName} 입니다.");
                return card;
            }
        }

        public class Player : Hand
        {
            public int score;
            public Player()
            {
                handCards = new List<Card>();
                score = 0;
            }
        }

        public class Dealer : Hand
        {
            public int score;

            public override Card DrawCard(ref List<Card> deck)
            {
                int cardIndex = new Random().Next(deck.Count);
                Card card = deck[cardIndex];
                deck.Remove(deck[cardIndex]);
                return card;
            }

            public Dealer()
            {
                handCards = new List<Card>();
                score = 0;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("블랙잭에 오신 걸 환영합니다.");
            Thread.Sleep(1000);
            Console.WriteLine("블랙잭은 점수의 합을 21점 가까이 맞추면 이기는 게임입니다.");
            Thread.Sleep(1000);
            Console.WriteLine("게임을 준비합니다.");
            //덱에 카드 넣기
            Deck deck = new Deck();
            deck.cards = Deck.LoadDeck(deck.cards, "Spade");
            deck.cards = Deck.LoadDeck(deck.cards, "Diamond");
            deck.cards = Deck.LoadDeck(deck.cards, "Club");
            deck.cards = Deck.LoadDeck(deck.cards, "Heart");

            //플레이어 패 뽑기
            Console.WriteLine("플레이어의 카드를 덱에서 뽑습니다.");
            Player player = new Player();
            player.handCards.Add(new Player().DrawCard(ref deck.cards));
            player.handCards.Add(new Player().DrawCard(ref deck.cards));
            Console.WriteLine($"당신의 카드는 {player.handCards[0].cardName}, {player.handCards[1].cardName} 입니다.");
            foreach (Card card in player.handCards)
            {
                player.score += card.cardValue;
            }
            Console.WriteLine($"현재 플레이어의 점수는 {player.score}점 입니다.\n");
            Thread.Sleep(1000);

            //딜러 패 뽑기
            Console.WriteLine("딜러의 카드를 덱에서 뽑습니다.");
            Dealer dealer = new Dealer();
            dealer.handCards.Add(new Dealer().DrawCard(ref deck.cards));
            dealer.handCards.Add(new Dealer().DrawCard(ref deck.cards));
            Console.WriteLine($"딜러의 카드 중 하나는 {dealer.handCards[0].cardName} 입니다.");
            foreach (Card card in dealer.handCards)
            {
                dealer.score += card.cardValue;
            }
            Thread.Sleep(1000);

            while (true)
            {
                if (player.score < 21)
                {
                    Console.Write("Hit 하시겠습니까 (y/n) ? : ");
                    string playerChoice = Console.ReadLine();
                    if (playerChoice == "y")
                    {
                        Console.WriteLine("플레이어의 카드를 덱에서 뽑습니다.");
                        Thread.Sleep(1000);
                        player.handCards.Add(new Player().DrawCard(ref deck.cards));
                        player.score += player.handCards[player.handCards.Count - 1].cardValue;
                        if (player.score > 21)
                        {
                            foreach (Card card in player.handCards)
                            {
                                if (card.cardValue == 11)
                                {
                                    card.cardValue = 1;
                                    player.score -= 10;
                                    break;
                                }
                            }
                            if (player.score > 21)
                            {
                                Console.WriteLine("Busted!");
                                break;
                            }
                        }
                        else if (player.score == 21)
                        {
                            Console.WriteLine("BlackJack!");
                            break;
                        }
                        Console.WriteLine($"현재 플레이어의 점수는 {player.score}점 입니다.\n");
                    }
                    else if (playerChoice == "n")
                    {
                        Console.WriteLine("플레이어가 Stay 합니다.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                    }
                }                
            }
            Console.WriteLine($"딜러의 나머지 카드는 {dealer.handCards[1].cardName} 입니다.");
            Console.WriteLine($"딜러의 점수는 {dealer.score} 입니다.\n");
            Thread.Sleep(1000);
            while (dealer.score < 17)
            {
                Console.WriteLine("딜러의 점수가 17보다 낮으므로 딜러가 Hit 합니다.");
                dealer.handCards.Add(new Player().DrawCard(ref deck.cards));
                dealer.score += dealer.handCards[dealer.handCards.Count - 1].cardValue;
                if (dealer.score > 21)
                {
                    foreach (Card card in dealer.handCards)
                    {
                        if (card.cardValue == 11)
                        {
                            card.cardValue = 1;
                            dealer.score -= 10;
                            break;
                        }
                    }
                    if (player.score > 21)
                    {
                        Console.WriteLine("Busted!");
                        break;
                    }
                }
                else if (player.score == 21)
                {
                    Console.WriteLine("BlackJack!");
                    break;
                }
                Console.WriteLine($"딜러의 점수는 {dealer.score} 입니다.\n");
            }

            if (dealer.score > player.score || player.score > 21)
            {
                Console.WriteLine("패배했습니다...");
            }
            else if (dealer.score < player.score)
            {
                Console.WriteLine("승리했습니다!");
            }
            else Console.WriteLine("무승부입니다.");
        }
    }
}