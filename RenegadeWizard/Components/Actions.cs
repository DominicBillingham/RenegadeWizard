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

        public int ActionThrow(Entity thrower, Entity item, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {thrower.Name} throws {item.Name} at {target.Name}");

            int actionCost = item.WhenThrown(target, thrower);

            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionConsume(Entity consumer, Entity edibleItem) 
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {consumer.Name} consumes {edibleItem.Name}");

            int actionCost = edibleItem.WhenConsumed(consumer);

            if (consumer == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionInspect(Entity inspector, Entity entity)
        {

            if (inspector.Conditions.Any(con => con is Madness))
            {
                // idea: write some custom lines for inspecting while mad
            }

            int actionCost = entity.WhenInspected();
            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionGrab(Entity grabber, Entity target)
        {
            Console.Write($" # {grabber.Name} grapples {target.Name}");

            int actionCost = target.WhenGrabbed(grabber);

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionKick(Entity kicker, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {kicker.Name} kicks {target.Name}");

            int actionCost = target.WhenKicked(kicker);

            Console.WriteLine("\n");
            return actionCost;
        }

        #region Synonyms
        public int ActionDrink(Entity consumer, Entity edibleItem)
        {
            return ActionConsume(consumer, edibleItem);
        }
        public int ActionEat(Entity consumer, Entity edibleItem)
        {
            return ActionConsume(consumer, edibleItem);
        }
        public int ActionShove(Entity shover, Entity target)
        {
            return ActionKick(shover, target);
        }
        public int ActionToss(Entity tosser, Entity item, Entity target)
        {
            return ActionThrow(tosser, item, target);
        }
        public int ActionInfo(Entity inspector, Entity entity)
        {
            return ActionInspect(inspector, entity);
        }

        #endregion

        public virtual void TakeTurn(Entity entity)
        {

        }


    }


}
