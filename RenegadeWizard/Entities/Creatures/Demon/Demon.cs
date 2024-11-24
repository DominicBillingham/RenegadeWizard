using RenegadeWizard.Components;
using RenegadeWizard;
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
    public class Demon : Creature
    {
        public Demon(string name)
        {
            Name = name;
            Description = "A very pissed off demon";
            Health = 8;
            Weight = 12;
            Attributes = new Attributes(15, 15, 15);
            Faction = Faction.Demon;
        }

        public override void TakeTurn()
        {
            var bite = new Interaction(this, "Devour").SelectRandomEnemy().ApplyDamage(4);
            bite.Description = $"{Name} devours poor [targets]";
            var claws = new Interaction(this, "Slash").SelectRandomEnemy().SelectRandomEnemy().ApplyDamage(3);
            claws.Description = $"{Name} swipes with jagged claws]";


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
