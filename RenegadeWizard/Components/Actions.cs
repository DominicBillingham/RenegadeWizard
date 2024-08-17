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

        //if (Invoker.Conditions.Any(con => con is Madness))
        //{
        //    item = Scene.GetRandomEntity();
        //    target = Scene.GetRandomEntity();
        //    item.WhenThrown(target, Invoker);
        //    Console.WriteLine();
        //    return 1;
        //}

        public int ActionThrow(Entity item, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Invoker.Name} throws {item.Name} at {target.Name}");

            int actionCost = item.WhenThrown(target, Invoker);

            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionConsume(Entity edibleItem) 
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Invoker.Name} consumes {edibleItem.Name}");

            int actionCost = edibleItem.WhenConsumed(Invoker);

            if (Invoker == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {

            if (Invoker.Conditions.Any(con => con is Madness))
            {
                // idea: write some custom lines for inspecting while mad
            }

            int actionCost = entity.WhenInspected();
            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionGrab(Entity target)
        {
            Console.Write($" # {Invoker.Name} grapples {target.Name}");

            int actionCost = target.WhenGrabbed(Invoker);

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionKick(Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Invoker.Name} kicks {target.Name}");

            int actionCost = target.WhenKicked(Invoker);

            Console.WriteLine("\n");
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
