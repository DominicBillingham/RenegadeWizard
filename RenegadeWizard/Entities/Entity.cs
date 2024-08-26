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

        private Factions faction;
        public Factions Faction { 
            get { return ModifierHelper.GetFactionAfterMods(this) ?? faction; } 
            set { faction = value; }
        } 
        public bool IsDestroyed { get { return Health < 1; } }
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
        public List<Modifier> ModifierImmunities { get; set; } = new List<Modifier>();

        // Composition Stuff
        public AgentActions? CharacterActions { get; set; }
        public Attributes? Attributes { get; set; }
        public Entity? HeldObject { get; set; }

        public int GetStrength()
        {
            return ModifierHelper.GetStrengthAfterMods(this);
        }
        public virtual void TakeTurn()
        {

        }

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
        public virtual int WhenInspected()
        {
            // idea: Make intellect requirements to get more details
            Console.Write($" #");

            if ( Attributes != null)
            {
                Console.Write($" STR:{GetStrength()}, AGI:{Attributes.Agility}, INT:{Attributes.Intellect} |");
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

            damage = ModifierHelper.GetDamageAfterMods(this, damage);

            if (HeldObject != null && HeldObject.IsDestroyed == false && ignoreArmour == false)
            {
                HeldObject.Health -= damage;
                HeldObject.BattleLog += $" -{damage}hp from {source} protecting {Name} |";

                Console.Write($" {Narrator.GetContrastWord()} {HeldObject.Name} blocks the blow!");

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

                WhenDamaged();

                if (IsDestroyed && ignoreArmour == false)
                {
                    SelfDestruct();
                }
            }
        }

        public virtual void WhenDamaged()
        {

        }

        public virtual void WhenHealed()
        {

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
