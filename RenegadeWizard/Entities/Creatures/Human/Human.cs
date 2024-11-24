using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures.Human
{
    public class Human : Creature
    {
        public Human(string name)
        {
            Name = name;
            Health = 5;
            Description = "A random, awesome person!";
            Attributes = new Attributes(10, 10, 10);
            Faction = Faction.Player;
        }

    }

}
