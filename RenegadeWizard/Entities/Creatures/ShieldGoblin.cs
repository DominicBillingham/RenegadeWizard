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
    public class ShieldGoblin : Creature
    {
        public ShieldGoblin(string name)
        {
            Name = name;
            Description = "As strong as they are dumb";
            Health = 5;
            Weight = 12;
            Actions = new ShieldGoblinActions(this);
            Attributes = new Attributes(12, 3, 3);
            Faction = Factions.Goblin;
        }

    }

    public class ShieldGoblinActions : Actions
    {
        public ShieldGoblinActions(Entity invoker) : base(invoker)
        {

        }

        public override void TakeTurn()
        {
            var rand = new Random();

            if (rand.Next(2) == 0)
            {
                ShieldAllies();
            }
            else
            {
                var enemy = Scene.GetRandomHostile(Invoker.Faction);
                ActionKick(enemy);
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
