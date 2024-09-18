using RenegadeWizard.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;
using System.Numerics;

namespace RenegadeWizard.Entities.Creatures
{
    public class Sheep : Creature
    {
        public Sheep(string name)
        {
            Name = name;
            Description = "It's fluffy fur makes you want to sleep";
            Health = 2;
            Weight = 3;
            Attributes = new Attributes(3, 3, 3);
            Faction = Factions.None;
        }

        public override void TakeTurn()
        {
            var baaaaa = new Interaction(this, "Baaaaa");
            baaaaa.Description = $"{Name} goes BAAAAAAAAAAAAAAAA";
            baaaaa.Execute();

        }

    }

}
