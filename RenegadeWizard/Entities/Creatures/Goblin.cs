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
            var interaction = new AgentActions();
            interaction.Agent = this;

            if (Random.Shared.Next(2) == 0)
            {
                var drink = new EntQuery().SelectDrinks().GetRandom();
                interaction.ActionConsume(drink);
            }
            else
            {
                var item = new EntQuery().SelectItems().GetRandom();
                var enemy = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Faction).GetRandom();
                interaction.ActionThrow(item, enemy);
            }

        }

    }

}
