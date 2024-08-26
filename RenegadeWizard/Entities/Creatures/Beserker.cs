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
    public class Beserker : Creature
    {
        public Beserker(string name)
        {
            Name = name;
            Description = "Raor";
            Health = 3;
            Weight = 12;
            Attributes = new Attributes(12, 3, 3);
            Faction = Factions.Goblin;
        }

        private int BonusDamage { get; set; } = 0;

        public override void TakeTurn()
        {

            var interaction = new BeserkerActions();
            interaction.Agent = this;

            var enemy = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Faction).GetRandom();
            interaction.ActionRecklessAttack(enemy, BonusDamage);

        }

        public override void WhenDamaged()
        {
            BonusDamage++;
        }

        public override void WhenHealed()
        {
            BonusDamage--;
        }

    }

    public class BeserkerActions : Interaction
    {
        public int ActionRecklessAttack(Entity target, int bonusDamage)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} brutall attacks {target.Name}");

            target.ApplyDamage(1 + bonusDamage, Agent.Name);
            target.ApplyCondition(new Wounded(1 + bonusDamage), Agent.Name);

            Console.WriteLine("\n");
            return 1;
        }

    }


}
