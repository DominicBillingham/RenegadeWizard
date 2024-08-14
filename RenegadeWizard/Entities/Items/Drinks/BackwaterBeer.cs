using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{
    public class BackwaterBeer : Item
    {
        public BackwaterBeer()
        {
            Name = "BackwaterBeer";
            Description = " # It's half empty, or is it half full?";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyHealing(2, $"thrown {Name}");
            return 1;
        }
        public override int WhenDrank(Entity drinker)
        {
            drinker.ApplyHealing(2, $"drinking {Name}");
            return 1;
        }

    }

}
