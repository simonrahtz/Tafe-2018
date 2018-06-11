using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public interface IView : CommandHandler<ViewCommand>
    {
        void DrawCard(Image background, Point coord);
        void ShowMessage(string message);
        void SetLabel(Label label, string text, int locationX, int locationY);

        
    }
}
