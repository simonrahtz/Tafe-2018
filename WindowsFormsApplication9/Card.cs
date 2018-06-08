using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum Value
    {
        ace=1, two, three, four, five, six, seven, eight,nine,ten,jack,queen,king
               
 
    }

    public enum Suit
    {
        hearts,diamonds,spades,clubs
    }

    
    public class Card
    {
        private Value value;
        private Suit suit;
        public Image cardFace;

        
        public Value Value
        {
            
            get { return value;}
                       
        }

       public Suit Suit
        {
            get { return suit; }
        }

        public Card(Suit suit, Value value)
        {
            this.value = value;
            this.suit = suit;
            cardFace = Image.FromFile(value.ToString() + "_of_" + suit.ToString() + ".png");
        }

        public int GetNumericValue()
        {
            return (value < Value.ten) ? (int)value : 10; //makes face cards equal 10
        }        
       
    }
   


}
