using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures.Misc
{
    public class Player : Creature
    {
        public Player(string name)
        {
            Name = name;
            Health = 20;
            Description = "Admiring yourself I see?";
            Attributes = new Attributes(10, 10, 10);
            Faction = Factions.Player;
            IsPlayerControlled = true;
        }

    }

}
