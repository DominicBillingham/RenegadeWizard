using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Enums;
using RenegadeWizard.GameClasses;
using static System.Net.Mime.MediaTypeNames;
using RenegadeWizard.Entities.Creatures;
using Microsoft.VisualBasic;
using System.Reflection;

namespace RenegadeWizard.Entities
{
    public class Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Weight { get; set; }
        public bool IsPlayerControlled { get; set; } = false;
        public string Description { get; set; } = string.Empty;
        public string BattleLog { get; set; } = string.Empty;
        public int DamageTakenLastRound { get; set; } = 0;
        public int HealingLastRound { get; set; } = 0;
        public Factions Faction { get; set; }
        public bool IsDestroyed { get { return Health < 1; } }
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
        public List<Modifier> ModifierImmunities { get; set; } = new List<Modifier>();

        // Composition Stuff
        public AgentActions? CharacterActions { get; set; }
        public Attributes? Attributes { get; set; }


        public Entity AfterMods()
        {
            Entity ent = new();

            ent.Faction = ModHelper.ModFaction(this);
            if (Attributes != null)
            {
                ent.Attributes = new Attributes(0,0,0);
                ent.Attributes.Strength = ModHelper.ModStrength(this);
                ent.Attributes.Agility = ModHelper.ModAgility(this);
                ent.Attributes.Intellect = ModHelper.ModIntellect(this);
            }

            return ent;
        }

        #region WhenMethods

        public virtual Entity GetTarget()
        {
            var target = ModHelper.ModTarget(this);
            return target;
        }

        public virtual void WhenDamaged()
        {

        }

        public virtual void WhenHealed()
        {

        }

        public virtual void TakeTurn()
        {

        }

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
        public virtual int WhenInspected()
        {
            // idea: Make intellect requirements to get more details
            Console.Write($" #");

            if ( Attributes != null)
            {
                Console.Write($" STR:{ AfterMods().Attributes.Strength}, AGI:{Attributes.Agility}, INT:{Attributes.Intellect} |");
            }

            Console.Write($" {Name} - {Description}");

            if (BattleLog != string.Empty)
            {
                Console.WriteLine();
                Console.Write(" #" + BattleLog);
            }

            return 0;
        }

        #endregion

        #region Health Methods
        public virtual void ApplyDamage(int damage, string source, bool ignoreArmour = false)
        {

            damage = ModHelper.ModDamage(this, damage);

            if (IsDestroyed == false)
            {
                DamageTakenLastRound += damage;
                Health -= damage;
                BattleLog += $" -{damage}hp from {source} |";
                WhenDamaged();
            }
        }

        public virtual void ApplyHealing(int heal, string? source = null)
        {
            Health += heal;
            BattleLog = $" +{heal}hp from {source} |";
            HealingLastRound += heal;
            WhenHealed();
        }

        #endregion

        #region Condition Methods

        public virtual void ApplyCondition(Modifier condition, string source)
        {
            if (IsDestroyed == false)
            {
                var existingCon = Modifiers.FirstOrDefault(con => con.GetType() == condition.GetType() );

                if (existingCon != null)
                {
                    existingCon.Duration += condition.Duration;
                    existingCon.OnApplication(this);
                } 
                else
                {
                    Modifiers.Add(condition);
                    condition.OnApplication(this);
                }

                BattleLog += $" gained {condition.Name}({condition.Duration}) from {source} |";

            }

        }

        #endregion

        public virtual void SelfDestruct()
        {
            Console.Write($"{Name} has been destroyed | ");
        }

    }

   

}
