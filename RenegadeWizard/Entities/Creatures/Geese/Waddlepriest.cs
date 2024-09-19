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
using RenegadeWizard.Entities.Creatures.Misc;

namespace RenegadeWizard.Entities.Creatures.Geese
{
    public class Waddlepriest : Creature
    {
        public Waddlepriest()
        {
            Name = GetName();
            Description = "May the goosefather bless us with sacred bread";
            Health = 3;
            Weight = 3;
            Attributes = new Attributes(3, 3, 7);
            Faction = Factions.Geese;
        }

        public override void TakeTurn()
        {
            var action = Random.Shared.Next(3);
            var anyDead = new EntQuery().SelectAllies(Faction).SelectCreatures().SelectDead().GetFirst();

            if (anyDead != null)
            {
                var revivify = new Interaction(this, "Revivify").SelectRandomDeadAlly().Resurrect();
                revivify.Description = $"{Name} brings back [targets] from the grave";
                revivify.Execute();

            }
            else
            {
                var heal = new Interaction(this, "HealingBurst").SelectAll().ApplyHealing(1);
                heal.Description = $"{Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring everyone's health!";
                heal.Execute();
            }

        }

        private string GetName()
        {
            string[] evilGeeseNames = new string[] { "MENACE", "TERROR", "HAVOC", "DOOM", "FURY", "CHAOS", "WRATH", "BANE", "CURSE", "GLOOM", "NIGHTMARE", "SHADE", "VILLAIN", "FIEND", "SCOURGE", "DREAD", "VEX", "MALIGN", "NEFARIOUS", "SINISTER", "MORBID", "DARKNESS", "PLAGUE", "HORROR", "PHANTOM", "GRIM", "RUIN", "HELLION", "MALEVOLENCE", "DISASTER", "SABOTEUR", "WRAITH", "INFERNO", "VENOM", "GRUDGE", "THREAT", "BLIGHT", "SPITE", "FEAR", "CATASTROPHE", "GREMLIN", "SERPENT", "CARNAGE", "RAVAGE", "BRIMSTONE", "TORMENT", "DEMON", "RAGE", "ABYSS", "FAMINE", "REVENGE", "BLOODLUST", "DEATH", "DEPRAVITY", "FURY", "MISERY", "WRONG", "BEAST", "EVIL", "BETRAYAL", "SHADOW", "MAYHEM", "MONSTER", "CALAMITY", "PUNISHER", "FEROCITY", "FURY", "MALEDICTION", "CORRUPTOR", "SIN", "CHAOTIC", "VOID", "TYRANT", "PESTILENCE", "INQUISITOR", "RUINATION", "SLAUGHTER", "BLASPHEMY", "VOIDLING", "CREEP", "DIABOLIC", "FEROCITY", "VICIOUS", "SINNER", "VIRUS", "VILLAINY", "SPECTER", "FIENDISH", "GHOUL", "POISON", "PLUNDER", "INSIDIOUS", "FERAL", "TERRORIST", "WARLOCK", "SATAN", "MALIGNANT", "TERRORIZER", "THUG", "BUTCHER", "SAVAGE", "RUTHLESS", "GORY", "WICKED" };

            return evilGeeseNames[Random.Shared.Next(evilGeeseNames.Count())];

        }

    }

}
