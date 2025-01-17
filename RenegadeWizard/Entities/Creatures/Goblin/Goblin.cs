﻿using RenegadeWizard.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;
using System.Numerics;

namespace RenegadeWizard.Entities.Creatures.Goblin
{
    public class Goblin : Creature
    {
        public Goblin(string name)
        {
            Name = name;
            Description = "A weak, silly and clumsy goblin - but they are crafty...";
            Health = 5;
            Weight = 12;
            Attributes = new Attributes(5, 5, 5);
            Faction = Faction.Goblin;
        }

        public override void TakeTurn()
        {
            var bite = new Interaction(this, "Bite").SelectRandomEnemy().ApplyDamage(3);
            bite.Description = $"{Name} bites [targets] hard!";
            var claws = new Interaction(this, "Claws").SelectRandomEnemy().ApplyDamage(2).ApplyCondition(new Bleeding(2));
            claws.Description = $"{Name} slashes [targets] with their cute claws!";


            if (Random.Shared.Next(2) == 0)
            {
                bite.Execute();
            }
            else
            {
                claws.Execute();
            }

        }

    }

}
