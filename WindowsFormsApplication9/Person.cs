﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public abstract class Person
    {
        protected List<Card> hand;
        protected int cardTotal;
        protected int numCards;
        protected Point cardLocation;
        


        public Person(Point cardLocation)
        {
            hand = new List<Card>();
            this.cardLocation = cardLocation;
        }

        public Point GetCardLocation()
        {
            cardLocation.X += 40;
            return cardLocation;             
            
        }

        public void SetCardLocation(int x)
        {
            cardLocation.X = x;
            
        }

        public List<Card> Hand()
        {
            return hand;
        }
       
        
        public int GetCardTotal
        {
            get { return cardTotal; }

        }
        public int GetNumCards
        {
            get { return numCards; }

        }
        public void AddCard(Card card)
        {
            hand.Add(card);
            numCards++;
            cardTotal += card.GetNumericValue();
        }
        public abstract Image GetCardFace();
       

    }
    public class Player : Person
    {
        private new Point cardLocation;

        public Player(Point cardLocation) : base(cardLocation)
        {
            this.cardLocation = cardLocation;
        }

        public override Image GetCardFace()
        {
            
            return hand[hand.Count - 1].cardFace;
        }

    }
    public class Dealer : Person
    {
        private new Point cardLocation;

        public Dealer(Point cardLocation) : base(cardLocation)
        {
            this.cardLocation = cardLocation;
        }


        public override Image GetCardFace()
        {
            
            if (numCards == 1)
            {
                return Image.FromFile("card_back.png");
            }

            return hand[hand.Count - 1].cardFace;
        }
    }



}
