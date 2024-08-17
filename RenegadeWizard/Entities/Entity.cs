﻿using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.Enums;
using RenegadeWizard.GameClasses;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeWizard.Entities
{
    public class Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; } = string.Empty;
        public string BattleLog { get; set; } = string.Empty;
        public int DamageTakenLastRound { get; set; } = 0;
        public Factions Faction { get; set; } = Factions.None;
        public bool IsDestroyed { get { return Health < 1; } }
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public List<Condition> ConditionImmunities { get; set; } = new List<Condition>();

        // Composition Stuff
        public Actions? Actions { get; set; }
        public Attributes? Attributes { get; set; }
        public Entity? HeldObject { get; set; }

        #region WhenAction Methods
        public virtual int WhenThrown(Entity target, Entity thrower)
        {
            Console.Write($" {Narrator.GetContrastWord()} it fails!");
            return 0;
        }
        public virtual int WhenConsumed(Entity consumer)
        {
            Console.Write($" {Narrator.GetContrastWord()} but it fails!");
            return 0;
        }
        public virtual int WhenGrabbed(Entity grabber)
        {
            Console.Write($" {Narrator.GetContrastWord()} but it fails!");
            return 0;
        }
        public virtual int WhenKicked(Entity kicker)
        {
            Console.Write($" {Narrator.GetContrastWord()} but it fails!");
            return 0;
        }
        public virtual int WhenInspected()
        {
            // idea: Make intellect requirements to get more details
            Console.Write($" #");

            if ( Attributes != null)
            {
                Console.Write($"STR:{Attributes.Strength}, AGI:{Attributes.Agility}, INT:{Attributes.Intellect} |");
            }

            Console.Write($" {Name} - {Description}");

            if (BattleLog != string.Empty)
            {
                Console.WriteLine();
                Console.Write(BattleLog);
            }

            return 0;
        }

        #endregion

        #region Health Methods
        public virtual void ApplyDamage(int damage, string? source = null)
        {
            if (Conditions.Any(con => con is Immortal))
            {
                BattleLog += $" -{damage}hp => 0hp from {source} because Immortal |";
                return;
            }

            if (Conditions.Any(con => con is Protected)) {
                damage = 1;
            }

            if (HeldObject != null && HeldObject.IsDestroyed == false)
            {
                HeldObject.Health -= damage;
                HeldObject.BattleLog += $" -{damage}hp from {source} protecting {Name} |";

                if ( HeldObject.IsDestroyed )
                {
                    HeldObject.SelfDestruct();
                }

                HeldObject = null;
                return;
            } 

            if (IsDestroyed == false)
            {
                DamageTakenLastRound += damage;
                Health -= damage;
                BattleLog += $" -{damage}hp from {source} |";
                if (IsDestroyed)
                {
                    SelfDestruct();
                }
            }
        }
        public virtual void ApplyHealing(int heal, string? source = null)
        {
            Health += heal;
            BattleLog = $" +{heal}hp from {source} |";
        }

        #endregion

        #region Condition Methods
        public virtual void ApplyConditionDamage(int damage, string? source = null)
        {
            if (Conditions.Any(con => con is Immortal))
            {
                BattleLog += $" -{damage}hp => 0hp from {source} because Immortal |";
                return;
            }

            // idea: Check for damage immunitiess
            if (IsDestroyed == false)
            {
                DamageTakenLastRound += damage;
                Health -= damage;
                BattleLog += $" -{damage}hp from {source} |";
                if (IsDestroyed)
                {
                    //Console.Write($"{Name} has been destroyed | ");
                }
            }
        }

        public virtual void ApplyCondition(Condition condition, string? source = null)
        {
            if (IsDestroyed == false)
            {
                BattleLog += $" gained {condition.Name} from {source} |";
                Conditions.Add(condition);
            }

        }

        public virtual void ApplyConditionEffects()
        {
            foreach (var con in Conditions)
            {

                if  ( ConditionImmunities.Any(conImmune => conImmune.GetType() == con.GetType() ) )
                {
                    continue;
                }

                con.ApplyEffect(this);
            }
            Conditions.RemoveAll(x => x.Duration <= 0);
        }

        #endregion

        public virtual void SelfDestruct()
        {
            Console.Write($"{Name} has been destroyed | ");
            Scene.Entities.Remove(this);
        }

    }

   

}
