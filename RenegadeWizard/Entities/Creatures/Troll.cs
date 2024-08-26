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
using Microsoft.VisualBasic;

namespace RenegadeWizard.Entities.Creatures
{
    public class Troll : Creature
    {
        public Troll(string name)
        {
            Name = name;
            Description = "As strong as they are dumb";
            Health = 5;
            Weight = 12;
            Attributes = new Attributes(12, 7, 3);
            Faction = Factions.Goblin;
        }


        public override void TakeTurn()
        {

            var trollActions = new TrollActions();
            var enemy = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(AfterMods().Faction).GetRandom();


            trollActions.ActionViolentClaws(this, enemy);

            if (Modifiers.Any(mod => mod is Burning))
            {
                ApplyCondition(new ChangedFaction(3, Factions.None), "burning");
            }
            else
            {
                ApplyHealing(2, "Regeneration");
            }

        }

    }

    public class TrollActions : AgentActions
    {
        public int ActionViolentClaws(Entity agent, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} brutally attacks {target.Name}");

            target = target.GetTarget();

            target.ApplyDamage(2, agent.Name);
            target.ApplyCondition(new Bleeding(2), agent.Name);

            Console.WriteLine("\n");
            return 1;
        }
    }


}
