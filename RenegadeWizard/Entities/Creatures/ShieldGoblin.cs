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

            var shieldActions = new ShieldGoblinActions();
            var ally = new EntQuery().SelectCreatures().SelectLiving().SelectAllies(Faction).GetRandom();
            shieldActions.ActionShieldAllies(this, ally);

        }

    }

    public class ShieldGoblinActions : AgentActions
    {
        public int ActionShieldAllies(Entity agent, Entity ally)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} valiantly protects {ally.Name}");
            ally.ApplyCondition(new Protected(2), $"{agent.Name}");
            Console.WriteLine("\n");
            return 1;
        }
    }


}
