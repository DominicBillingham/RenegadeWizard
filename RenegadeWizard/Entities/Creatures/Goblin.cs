using RenegadeWizard.Components;
using RenegadeWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Entities.Creatures
{
    public class Goblin : Creature
    {
        public Goblin(string name)
        {
            Name = name;
            Description = "A weak, silly and clumsy goblin";
            Health = 5;
            Weight = 12;
            Actions = new Actions(this);
            Attributes = new Attributes(5, 5, 5);
            Faction = Factions.Goblin;
        }

    }

}
