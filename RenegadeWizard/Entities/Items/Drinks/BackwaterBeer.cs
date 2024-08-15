﻿using System;
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
            Description = "It's half empty, or is it half full?";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {

            return 1;
        }
        public override int WhenConsumed(Entity consume)
        {
            consume.ApplyHealing(2, $"consuming {Name}");
            return 1;
        }
        public override void SelfDestruct()
        {
            base.SelfDestruct();
            Scene.GetRandomCreature().ApplyHealing(2, $"{Name} splashing randomly");
        }

    }

}
