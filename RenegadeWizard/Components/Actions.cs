using RenegadeWizard.Conditions;
using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Components
{
    public class Actions
    {
        protected Entity Invoker { get; set; }
        public Actions(Entity invoker) 
        { 
            Invoker = invoker;
        }

        public virtual void TakeTurn()
        {
            Console.WriteLine($" # {Invoker.Name} is unable to do anything!");
        }

        public int ActionThrow(Entity item, Entity target)
        {
            Console.Write(" # ");

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                item = Scene.GetRandomEntity();
                target = Scene.GetRandomEntity();
                item.WhenThrown(target, Invoker);
                Console.WriteLine();
                return 1;
            }

            int actionCost = item.WhenThrown(target, Invoker);
            Console.WriteLine();
            return actionCost;

        }
        public int ActionConsume(Entity edibleItem) 
        {
            Console.Write(" # ");

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                edibleItem = Scene.GetRandomEntity();
                edibleItem.WhenConsumed(Invoker);
                Console.WriteLine();
                return 1;
            }

            int actionCost = edibleItem.WhenConsumed(Invoker);

            if (Invoker == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine();
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {
            Console.Write(" # ");

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                // idea: write some custom lines for inspecting while mad
            }

            int actionCost = entity.WhenInspected();
            Console.WriteLine();
            return actionCost;
        }
        public int ActionGrab(Entity target)
        {

            Console.Write(" # ");

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                target = Scene.GetRandomEntity();
                target.WhenGrabbed(Invoker);
                Console.WriteLine();
                return 1;
            }

            int actionCost = target.WhenGrabbed(Invoker);
            Console.WriteLine();
            return actionCost;
        }
        public int ActionKick(Entity target)
        {
            Console.Write($" # {Narrator.GetConnector()} {Invoker.Name} kicks {target.Name} ");

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                target = Scene.GetRandomEntity();
                target.WhenKicked(Invoker);
                Console.WriteLine();
                return 1;
            }

            int actionCost = target.WhenKicked(Invoker);
            Console.WriteLine();
            return actionCost;
        }

        #region Synonyms
        public int ActionDrink(Entity edibleItem)
        {
            return ActionConsume(edibleItem);
        }
        public int ActionEat(Entity edibleItem)
        {
            return ActionConsume(edibleItem);
        }
        public int ActionShove(Entity target)
        {
            return ActionKick(target);
        }
        public int ActionToss(Entity item, Entity target)
        {
            return ActionThrow(item, target);
        }
        #endregion


    }
}
