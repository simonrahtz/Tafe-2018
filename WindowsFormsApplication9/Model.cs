using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Model
    {
        protected LinkedList<Card> deck;
        protected List<Card> deck2;
        private CommandHandler<ViewCommand> commandHandler;
        private int cardIndex;
        private int deckSize; 
        private Random rnd;
        private string message;
        private Player playerOne;
        private Dealer dealer;
            

       

        public string Message
        {
            get { return message;   }

        }

        public Player PlayerOne
        {
            get
            {
                return playerOne;
            }

           
        }

        public Model(CommandHandler<ViewCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
            deck = new LinkedList<Card>();
            deck2 = new List<Card>();
            deckSize = 52;
            rnd = new Random();
            
        }

        public void Handle(ModelCommand command)

        {
            command.execute(this);

        }


        public void Start()
        {
                               
            CreateDeck();
            playerOne = new Player(new Point(0,50));
            dealer = new Dealer(new Point(400,50));
            
        }
        
        public void DealPlayer()
        {
            UpdatePerson(playerOne);
        }

        public void DealDealer()
        {
            UpdatePerson(dealer);
        }

        public void StandEvent()
        {
            UpdateFirstTwoCards();
                        

            while (dealer.GetCardTotal < 17)
            {
                DealDealer();
                
                if (dealer.GetCardTotal > 21)
                {
                    message = "Dealer Total: " + dealer.GetCardTotal.ToString() + " Dealer Busts!";
                    return;
                }
            }
            
            switch (dealer.GetCardTotal.CompareTo(playerOne.GetCardTotal))
            {
                case -1:message = "Dealer Total: " + dealer.GetCardTotal.ToString() + " You Win!";
                    break;
                case 1:message = "Dealer Total: " + dealer.GetCardTotal.ToString() + " You Lose!";
                    break;
                case 0:message = "Dealer Total: " + dealer.GetCardTotal.ToString() + " Push";
                    break;
                default:
                    break;
            }

            
        }
        public void UpdateFirstTwoCards()
        {
            dealer.SetCardLocation(400);

            for (int i = 0; i < 2; i++)
            {
                
                Card card = dealer.Hand()[i];

                Update(dealer.GetCardLocation(), card.cardFace);
            }
        }


        
        public void UpdatePerson(Person person)
        {
            cardIndex = rnd.Next(deckSize);
            deckSize--;
            person.AddCard(deck.Get(cardIndex));
            
            
            Update(person.GetCardLocation(),person.GetCardFace());
        }
       
                
       
        public void CreateDeck()
        {
            
            for (Value value = Value.ace; value <= Value.king; value++)
            {
                for (Suit suit = Suit.hearts; suit <= Suit.clubs; suit++)
                {
                    deck.Add(new Card(suit, value));
                    deck2.Add(new Card(suit, value));
                }
            }
            
        }

        public Boolean Result()
        {

            if (PlayerOne.GetCardTotal > 21)
            {
                message = "Bust";

                UpdateFirstTwoCards();
                    
                return true;
            }
            else if (PlayerOne.GetCardTotal == 21)
            {
                message = "BlackJack";

                UpdateFirstTwoCards();

                StandEvent();

                return true;
            }
            else
                      
            return false;
            
            
        }
       

        public void Update(Point coord,Image cardFace)
        {
            commandHandler.Handle(new DrawCardCommand(coord, cardFace)); //draw random card from deck

            deck.Remove(cardIndex);  
            
        }
    }
}
