using RenegadeWizard.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

namespace RenegadeWizard.Entities.Creatures.Geese
{
    public class Goozerker : Creature
    {
        public Goozerker()
        {
            Name = GetName();
            Description = "It's WIEDLING AN AXE";
            Health = 5;
            Weight = 5;
            Attributes = new Attributes(9, 4, 1);
            Faction = Factions.Geese;
        }

        int DamageCount = 0;

        public override void TakeTurn()
        {

            var action = Random.Shared.Next(2);

            if (action == 0)
            {
                var axeSwing = new Interaction(this, "UltimateAxe").SelectRandomEnemy().ApplyDamage(1 + DamageCount);
                axeSwing.Description = $"{Name} brings their colossal axe down";
                axeSwing.Execute();
            }

            if (action == 1)
            {
                var devour = new Interaction(this, "Devour").SelectRandomAlly().Devour();
                devour.Description = $"{Name} attempts to devour one of their geese friends! ";
                devour.Execute();
            }

        }

        public override void WhenDamaged()
        {
            DamageCount++;
        }

        private string GetName()
        {
            string[] evilGeeseNames = new string[] { "MENACE", "TERROR", "HAVOC", "DOOM", "FURY", "CHAOS", "WRATH", "BANE", "CURSE", "GLOOM", "NIGHTMARE", "SHADE", "VILLAIN", "FIEND", "SCOURGE", "DREAD", "VEX", "MALIGN", "NEFARIOUS", "SINISTER", "MORBID", "DARKNESS", "PLAGUE", "HORROR", "PHANTOM", "GRIM", "RUIN", "HELLION", "MALEVOLENCE", "DISASTER", "SABOTEUR", "WRAITH", "INFERNO", "VENOM", "GRUDGE", "THREAT", "BLIGHT", "SPITE", "FEAR", "CATASTROPHE", "GREMLIN", "SERPENT", "CARNAGE", "RAVAGE", "BRIMSTONE", "TORMENT", "DEMON", "RAGE", "ABYSS", "FAMINE", "REVENGE", "BLOODLUST", "DEATH", "DEPRAVITY", "FURY", "MISERY", "WRONG", "BEAST", "EVIL", "BETRAYAL", "SHADOW", "MAYHEM", "MONSTER", "CALAMITY", "PUNISHER", "FEROCITY", "FURY", "MALEDICTION", "CORRUPTOR", "SIN", "CHAOTIC", "VOID", "TYRANT", "PESTILENCE", "INQUISITOR", "RUINATION", "SLAUGHTER", "BLASPHEMY", "VOIDLING", "CREEP", "DIABOLIC", "FEROCITY", "VICIOUS", "SINNER", "VIRUS", "VILLAINY", "SPECTER", "FIENDISH", "GHOUL", "POISON", "PLUNDER", "INSIDIOUS", "FERAL", "TERRORIST", "WARLOCK", "SATAN", "MALIGNANT", "TERRORIZER", "THUG", "BUTCHER", "SAVAGE", "RUTHLESS", "GORY", "WICKED" };

            return evilGeeseNames[Random.Shared.Next(evilGeeseNames.Count())];

        }

    }

}
