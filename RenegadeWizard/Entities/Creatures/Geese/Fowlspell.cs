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
    public class Fowlspell : Creature
    {
        public Fowlspell()
        {
            Name = GetName();
            Description = "It's wearing an adorable little pointed wizard hat...";
            Health = 3;
            Weight = 3;
            Attributes = new Attributes(2, 2, 8);
            Faction = Factions.Geese;
        }

        public override void TakeTurn()
        {
            var action = Random.Shared.Next(3);

            if (action == 0)
            {
                var fireball = new Interaction(this, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));
                fireball.Description = $"{Name} casts a {Narrator.GetPowerfulWord()} fireball, setting [targets] on fire!";
                fireball.Execute();
            }

            if (action == 1)
            {
                var thunderstorm = new Interaction(this, "ThunderStorm").SelectRandom().SelectRandom().SelectRandom().ApplyDamage(2);
                thunderstorm.Description = $"{Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning, striking [targets]!";
                thunderstorm.Execute();
            }

            if (action == 2)
            {
                var magicMissle = new Interaction(this, "ArcaneMissle").SelectRandomEnemy().ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
                magicMissle.Description = $"{Name} casts a {Narrator.GetPowerfulWord()} missle barrage at [targets]!";
                magicMissle .Execute();
            }

        }


        private string GetName()
        {
            string[] evilGeeseNames = new string[] { "MENACE", "TERROR", "HAVOC", "DOOM", "FURY", "CHAOS", "WRATH", "BANE", "CURSE", "GLOOM", "NIGHTMARE", "SHADE", "VILLAIN", "FIEND", "SCOURGE", "DREAD", "VEX", "MALIGN", "NEFARIOUS", "SINISTER", "MORBID", "DARKNESS", "PLAGUE", "HORROR", "PHANTOM", "GRIM", "RUIN", "HELLION", "MALEVOLENCE", "DISASTER", "SABOTEUR", "WRAITH", "INFERNO", "VENOM", "GRUDGE", "THREAT", "BLIGHT", "SPITE", "FEAR", "CATASTROPHE", "GREMLIN", "SERPENT", "CARNAGE", "RAVAGE", "BRIMSTONE", "TORMENT", "DEMON", "RAGE", "ABYSS", "FAMINE", "REVENGE", "BLOODLUST", "DEATH", "DEPRAVITY", "FURY", "MISERY", "WRONG", "BEAST", "EVIL", "BETRAYAL", "SHADOW", "MAYHEM", "MONSTER", "CALAMITY", "PUNISHER", "FEROCITY", "FURY", "MALEDICTION", "CORRUPTOR", "SIN", "CHAOTIC", "VOID", "TYRANT", "PESTILENCE", "INQUISITOR", "RUINATION", "SLAUGHTER", "BLASPHEMY", "VOIDLING", "CREEP", "DIABOLIC", "FEROCITY", "VICIOUS", "SINNER", "VIRUS", "VILLAINY", "SPECTER", "FIENDISH", "GHOUL", "POISON", "PLUNDER", "INSIDIOUS", "FERAL", "TERRORIST", "WARLOCK", "SATAN", "MALIGNANT", "TERRORIZER", "THUG", "BUTCHER", "SAVAGE", "RUTHLESS", "GORY", "WICKED" };

            return evilGeeseNames[Random.Shared.Next(evilGeeseNames.Count())];

        }

    }

}
