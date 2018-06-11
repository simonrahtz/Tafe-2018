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
        private LinkedList<Card> deck;
        private CommandHandler<ViewCommand> commandHandler;
        private int cardIndex;
        private int deckSize; 
        private Random rnd;
        private Player playerOne;
        private Dealer dealer;
            

        public Player PlayerOne
        {
            get { return playerOne; }

           
        }
        public void Deal()
        {
            for (int i = 0; i < 2; i++)
            {
                UpdatePerson(dealer);
                UpdatePerson(PlayerOne);
            }
        }
        public void Hit()
        {
            UpdatePerson(playerOne);
            Result();
           

        }
        

        public Model(CommandHandler<ViewCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
            deck = new LinkedList<Card>();
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
            playerOne = new Player(new Point(0,70),"red");
            dealer = new Dealer(new Point(400,70),"red");
            
        }
        
        public void StandEvent()
        {
            UpdateFirstTwoCards();

            
            while (dealer.GetCardTotal < 17)
            {
                UpdatePerson(dealer);
                
                if (dealer.GetCardTotal > 21)
                {
                    Update("Dealer Total: " + dealer.GetCardTotal.ToString() + " Dealer Busts!");
                    return;
                }
            }
            
            switch (dealer.GetCardTotal.CompareTo(playerOne.GetCardTotal))
            {
                case -1: Update("Dealer Total: " + dealer.GetCardTotal.ToString() + " You Win!");
                    break;
                case 1:  Update("Dealer Total: " + dealer.GetCardTotal.ToString() + " You Lose!");
                    break;
                case 0:  Update("Dealer Total: " + dealer.GetCardTotal.ToString() + " Push");
                    break;
                default:
                    break;
            }
            
            

        }
        private void UpdateFirstTwoCards()
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
       
                
       
        private void CreateDeck()
        {
            
            for (Value value = Value.ace; value <= Value.king; value++)
            {
                for (Suit suit = Suit.hearts; suit <= Suit.clubs; suit++)
                {
                    deck.Add(new Card(suit, value));
                }
            }
            
        }

        public Boolean Result()
        {

            if (PlayerOne.GetCardTotal > 21)
            {
                UpdateFirstTwoCards();
                Update("Bust");

                
                    
                return true;
            }
            else if (PlayerOne.GetCardTotal == 21)
            {
                UpdateFirstTwoCards();
                Update("BlackJack");
                                

                StandEvent();

                return true;
            }
            else
                      
            return false;
            
            
        }
       

        private void Update(Point coord,Image cardFace)
        {
            commandHandler.Handle(new DrawCardCommand(coord, cardFace)); //draw random card from deck

            deck.Remove(cardIndex);  
            
        }
        private void Update(string message)
        {
            commandHandler.Handle(new DrawMessageBox(message));
        }
    }
}
