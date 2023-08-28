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
            Console.WriteLine("플레이어의 카드를 덱에서 뽑습니다.");
            Console.WriteLine(deck.cards[25].cardName);
            Thread.Sleep(1000);
        }
    }
}