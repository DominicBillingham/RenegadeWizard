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
            Actions = new GoblinActions(this);
            Attributes = new Attributes(5, 5, 5);
            Faction = Factions.Goblin;
        }

    }

    public class GoblinActions : Actions
    {
        public GoblinActions(Entity invoker) : base(invoker)
        {

        }

        public override void TakeTurn()
        {
            var rand = new Random();

            if (rand.Next(2) == 0)
            {
                var drink = Scene.GetRandomEdibleItem();
                ActionConsume(drink);
            }
            else
            {
                var item = Scene.GetRandomItem();
                var enemy = Scene.GetRandomHostile(Invoker.Faction);
                ActionThrow(item, enemy);
            }

        }

        public void ShieldAllies()
        {
            var ally = Scene.GetRandomAlly(Invoker.Faction);
            ally.ApplyCondition(new Protected(2), $"{Invoker.Name}");
            Console.Write($" # {Narrator.GetConnectorWord()} {Invoker.Name} valiantly protects {ally.Name}");
            Console.WriteLine("\n");

        }

    }

}
