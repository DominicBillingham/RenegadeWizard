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
    public class Demon : Creature
    {
        public Demon(string name)
        {
            Name = name;
            Description = "A very angry, pissed off demon";
            Health = 7;
            Weight = 15;
            CharacterActions = new Interaction();
            Attributes = new Attributes(12, 12, 12);
            ModifierImmunities.Add(new Burning(3));
            Modifiers.Add(new Burning(999));
            Faction = Factions.Demon;
        }

    }

}
