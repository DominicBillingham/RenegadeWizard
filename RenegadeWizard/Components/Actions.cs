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
        public Actions(Entity invoker) { 
            Invoker = invoker;
        }
        public int ActionThrow(Entity item, Entity target) {
            int actionCost = item.WhenThrown(target, Invoker);
            return actionCost;
        }
        public int ActionDrink(Entity drink) {
            int actionCost = drink.WhenDrank(Invoker);
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {
            int actionCost = entity.WhenInspected();
            return actionCost;
        }
        public int ActionGrab(Entity entity)
        {
            int actionCost = entity.WhenGrabbed(Invoker);
            return actionCost;
        }
        public int ActionKick(Entity entity)
        {
            int actionCost = entity.WhenKicked(Invoker);
            return actionCost;
        }

    }
}
