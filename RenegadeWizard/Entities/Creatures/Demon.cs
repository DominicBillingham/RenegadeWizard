﻿using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
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
            CharacterActions = new Actions();
            Attributes = new Attributes(12, 12, 12);
            ConditionImmunities.Add(new Burning(3));
            Conditions.Add(new Burning(999));
            Faction = Factions.Demon;
        }

    }

}
