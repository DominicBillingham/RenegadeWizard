using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{
    public class BackwaterBeer : Drink
    {
        public BackwaterBeer()
        {
            Name = "BackwaterBeer";
            Description = "It's half empty, or is it half full?";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyHealing(2, $"thrown {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override int WhenConsumed(Entity consume)
        {
            consume.ApplyHealing(2, $"consuming {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }

    }

}
