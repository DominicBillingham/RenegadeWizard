﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{
    public class GreatBritishTea : Drink
    {
        public GreatBritishTea()
        {
            Name = "GreatBritishTea";
            Description = "The finest in the world";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyCondition(new Immortal(1), $"{Name} thrown by {thrower.Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override int WhenConsumed(Entity consumer)
        {
            consumer.ApplyCondition(new Immortal(1), $"consuming {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }

    }
}
