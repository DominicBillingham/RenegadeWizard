﻿using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Toady : Creature
    {
        public Toady(string name)
        {
            Name = name;
            Description = "He looks like a FUN GUY";
            Health = 3;
            Weight = 6;
            Actions = new Actions(this);
            Attributes = new Attributes(4, 7, 13);

        }
        public override int WhenConsumed(Entity consumer)
        {
            consumer.ApplyCondition(new Madness(3));
            return 1;
        }
        public override int WhenKicked(Entity kicker)
        {
            var creatures = Scene.GetCreatures();
            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Madness(2), $"{kicker.Name} kicking {Name}");
            }
            return 1;
        }


    }

}