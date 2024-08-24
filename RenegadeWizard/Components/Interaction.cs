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
    public class Interaction
    {
        public Entity Agent { get; set; }

        public int ActionThrow(Entity item, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} throws {item.Name} at {target.Name}");

            int actionCost = item.WhenThrown(target, Agent);

            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionConsume(Entity edibleItem)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} consumes {edibleItem.Name}");

            int actionCost = edibleItem.WhenConsumed(Agent);

            if (Agent == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {

            if (Agent.Conditions.Any(con => con is Madness))
            {
                // idea: write some custom lines for inspecting while mad
            }

            int actionCost = entity.WhenInspected();
            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionGrab(Entity target)
        {
            Console.Write($" # {Agent.Name} grapples {target.Name}");

            int actionCost = target.WhenGrabbed(Agent);

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionKick(Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} kicks {target.Name}");

            int actionCost = target.WhenKicked(Agent);

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
        public int ActionInfo(Entity entity)
        {
            return ActionInspect(entity);
        }

        #endregion


    }


}
