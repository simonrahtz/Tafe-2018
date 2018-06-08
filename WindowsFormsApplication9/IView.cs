using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public interface IView : CommandHandler<ViewCommand>
    {
        void DrawCard(Image background, Point coord);

        
    }
}
