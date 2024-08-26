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
            Attributes = new Attributes(5, 5, 5);
            Faction = Factions.Goblin;
        }

        public override void TakeTurn()
        {
            var goblinActions = new AgentActions();

            if (Random.Shared.Next(2) == 0)
            {
                var drink = new EntQuery().SelectDrinks().GetRandom();
                goblinActions.ActionConsume(this, drink);
            }
            else
            {
                var item = new EntQuery().SelectItems().GetRandom();
                var target = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles( AfterMods().Faction ).GetRandom();
                target = target.GetTarget();
                goblinActions.ActionThrow(this, item, target);
            }

        }

    }

}
