﻿using RenegadeWizard.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;
using System.Numerics;

namespace RenegadeWizard.Entities.Creatures.Geese
{
    public class Goose : Creature
    {
        public Goose()
        {
            Name = GetName();
            Description = "HOOOOOOOOOOOOOOOOOOOOONNNNNNNNNNNKK";
            Health = 3;
            Weight = 3;
            Attributes = new Attributes(5, 6, 3);
            Faction = Factions.Geese;
        }

        public override void TakeTurn()
        {
            var peck = new Interaction(this, "Bite").SelectRandomEnemy().ApplyDamage(2);
            peck.Description = $"{Name} bites you, and it stings like a bitch.";
            peck.Execute();
        }

        public override void WhenDamaged()
        {
            var peck = new Interaction(this, "Retalitory Peck").SelectRandomEnemy().ApplyDamage(1);
            peck.Description = $"{Name} bites back!";
            peck.Execute();
        }

        private string GetName()
        {
            string[] evilGeeseNames = new string[] { "MENACE", "TERROR", "HAVOC", "DOOM", "FURY", "CHAOS", "WRATH", "BANE", "CURSE", "GLOOM", "NIGHTMARE", "SHADE", "VILLAIN", "FIEND", "SCOURGE", "DREAD", "VEX", "MALIGN", "NEFARIOUS", "SINISTER", "MORBID", "DARKNESS", "PLAGUE", "HORROR", "PHANTOM", "GRIM", "RUIN", "HELLION", "MALEVOLENCE", "DISASTER", "SABOTEUR", "WRAITH", "INFERNO", "VENOM", "GRUDGE", "THREAT", "BLIGHT", "SPITE", "FEAR", "CATASTROPHE", "GREMLIN", "SERPENT", "CARNAGE", "RAVAGE", "BRIMSTONE", "TORMENT", "DEMON", "RAGE", "ABYSS", "FAMINE", "REVENGE", "BLOODLUST", "DEATH", "DEPRAVITY", "FURY", "MISERY", "WRONG", "BEAST", "EVIL", "BETRAYAL", "SHADOW", "MAYHEM", "MONSTER", "CALAMITY", "PUNISHER", "FEROCITY", "FURY", "MALEDICTION", "CORRUPTOR", "SIN", "CHAOTIC", "VOID", "TYRANT", "PESTILENCE", "INQUISITOR", "RUINATION", "SLAUGHTER", "BLASPHEMY", "VOIDLING", "CREEP", "DIABOLIC", "FEROCITY", "VICIOUS", "SINNER", "VIRUS", "VILLAINY", "SPECTER", "FIENDISH", "GHOUL", "POISON", "PLUNDER", "INSIDIOUS", "FERAL", "TERRORIST", "WARLOCK", "SATAN", "MALIGNANT", "TERRORIZER", "THUG", "BUTCHER", "SAVAGE", "RUTHLESS", "GORY", "WICKED" };

            return evilGeeseNames[Random.Shared.Next(evilGeeseNames.Count())];

        }

    }

}