using System;
using System.Drawing;
using System.Windows.Forms;

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
    public class DrawMessageBox : ViewCommand

    {
        string message;

        public DrawMessageBox(String message)
        {
            this.message = message;
        }

        public override void execute(IView view)
        {
            view.ShowMessage(message);
        }
    }
    public class SetLabel : ViewCommand

    {
        Label label;
        string text;
        int locationX, locationY;

        public SetLabel(Label label, string text, int locationX, int locationY)
        {
            this.label = label;
            this.text = text;
            this.locationX = locationX;
            this.locationY = locationY;
        }

        public override void execute(IView view)
        {
            view.SetLabel(label, text, locationX, locationY);
        }
    }
}