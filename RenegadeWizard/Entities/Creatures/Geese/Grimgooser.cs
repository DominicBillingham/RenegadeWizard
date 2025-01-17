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
using static System.Collections.Specialized.BitVector32;
using RenegadeWizard.Entities.Creatures.Misc;

namespace RenegadeWizard.Entities.Creatures.Geese
{
    public class Grimgooser : Creature
    {
        public Grimgooser()
        {
            Name = GetName();
            Description = "Looks like they're going to have the last quack";
            Health = 5;
            Weight = 5;
            Attributes = new Attributes(7, 10, 8);
            Faction = Faction.Geese;
        }

        int DamageCount = 0;

        public override void TakeTurn()
        {

            var action = Random.Shared.Next(2);

            if (action == 0)
            {
                var leech = new Interaction(this, "LifeSteal").SelectAll().ApplyDamage(1).Lifesteal();
                leech.Description = $"{Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
                leech.Execute();
            }

            if (action == 1)
            {
                var raiseDead = new Interaction(this, "RaiseDead").SelectDeadCreatures().RaiseDead();
                raiseDead.Description = $"{Name} raises an undead army from the corpses of their allies!";
                raiseDead.Execute();
            }

        }

        public override void WhenDamaged(Interaction? trigger = null)
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
