using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public abstract class ModelCommand : ICommand<Model>
    {
        public abstract void execute(Model model);
    }
    class StartGameCommand : ModelCommand
    {
        public override void execute(Model model)
        {
            model.Start();
        }
    }
    class DealCards : ModelCommand
    {
        public override void execute(Model model)
        {
            model.Deal();
        }
    }
    class Hit : ModelCommand
    {
        public override void execute(Model model)
        {
            model.Hit();
        }
    }
    class Stand : ModelCommand
    {
        public override void execute(Model model)
        {
            model.StandEvent();
        }
    }


}
