using System;
using System.Drawing;

namespace BlackJack
{
    public abstract class ViewCommand : ICommand<IView>
    {
        public abstract void execute(IView view);
    }

    public class DrawCardCommand : ViewCommand
    {
        
        private Point coord;
        private Image cardFace;


        public DrawCardCommand(Point coord, Image cardFace)
        {
            this.coord = coord;
            this.cardFace = cardFace;
        }
        
        public override void execute(IView view)
        {
            Image cardImage = cardFace;

            view.DrawCard(cardImage, coord);
        }
    }
}