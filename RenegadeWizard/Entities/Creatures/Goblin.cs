using RenegadeWizard.Components;
using RenegadeWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Creatures
{
    public class Goblin : Creature
    {
        public Goblin(string name)
        {
            Name = name;
            Description = "A weak, silly and clumsy goblin - but they are crafty...";
            Health = 5;
            Weight = 12;
            CharacterActions = new GoblinActions();
            Attributes = new Attributes(5, 5, 5);
            Faction = Factions.Goblin;
        }




    }

    public class GoblinActions : Actions
    {
        public override void TakeTurn(Entity entity)
        {
            var rand = new Random();

            if (rand.Next(2) == 0)
            {
                var drink = Scene.GetRandomEdibleItem();
                ActionConsume(entity, drink);
            }
            else
            {
                var item = Scene.GetRandomItem();
                var enemy = Scene.GetRandomHostile(entity.Faction);
                ActionThrow(entity, item, enemy);
            }

        }
    }

}
