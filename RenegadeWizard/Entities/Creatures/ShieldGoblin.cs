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
            Attributes = new Attributes(12, 3, 3);
            Faction = Factions.Goblin;
        }

        public override void TakeTurn()
        {

            var interaction = new ShieldGoblinActions();
            interaction.Agent = this;

            if (Random.Shared.Next(2) == 0)
            {
                var ally = Scene.GetRandomAlly(Faction);
                interaction.ActionShieldAllies(ally);

            }
            else
            {
                var enemy = Scene.GetRandomHostile(Faction);
                interaction.ActionKick(enemy);
            }

        }

    }

    public class ShieldGoblinActions : Interaction
    {
        public int ActionShieldAllies(Entity ally)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} valiantly protects {ally.Name}");
            ally.ApplyCondition(new Protected(2), $"{Agent.Name}");
            Console.WriteLine("\n");
            return 1;
        }
    }


}
