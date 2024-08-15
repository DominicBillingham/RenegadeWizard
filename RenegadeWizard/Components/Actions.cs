using RenegadeWizard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Components
{
    public class Actions
    {
        private Entity Invoker { get; set; }
        public Actions(Entity invoker) 
        { 
            Invoker = invoker;
        }
        public int ActionThrow(Entity item, Entity target)
        {
            Console.Write(" # ");
            int actionCost = item.WhenThrown(target, Invoker);

            Console.WriteLine();
            return actionCost;
        }
        public int ActionConsume(Entity edibleItem) 
        {
            Console.Write(" # ");
            int actionCost = edibleItem.WhenConsumed(Invoker);

            Console.WriteLine();
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {
            Console.Write(" # ");
            int actionCost = entity.WhenInspected();

            Console.WriteLine();
            return actionCost;
        }
        public int ActionGrab(Entity target)
        {
            Console.Write(" # ");
            int actionCost = target.WhenGrabbed(Invoker);

            Console.WriteLine();
            return actionCost;
        }
        public int ActionKick(Entity target)
        {
            Console.Write(" # ");
            int actionCost = target.WhenKicked(Invoker);

            Console.WriteLine();
            return actionCost;
        }

    }
}
