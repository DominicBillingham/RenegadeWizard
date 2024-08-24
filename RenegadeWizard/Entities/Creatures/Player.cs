using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Player : Creature
    {
        public Player(string name)
        {
            Name = name;
            Health = 777;
            Description = "Admiring yourself I see?";
            CharacterActions = new Interaction();
            Attributes = new Attributes(10, 10, 10);
            Faction = Factions.Player;
        }

    }

}
