using RenegadeWizard.Components;
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

        public event 

        // Composition Stuff
        public Interaction? CharacterActions { get; set; }
        public Attributes? Attributes { get; set; }
        public Entity? HeldObject { get; set; }

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
                Console.Write($" STR:{Attributes.Strength}, AGI:{Attributes.Agility}, INT:{Attributes.Intellect} |");
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
            if (Conditions.Any(con => con is Immortal))
            {
                BattleLog += $" -{damage}hp => 0hp from {source} because Immortal |";
                return;
            }

            if (Conditions.Any(con => con is Protected)) {
                damage -= 1;
            }

            if (Conditions.Any(con => con is Wounded))
            {
                damage += 1;
            }

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

        public virtual void ApplyHealing(int heal, string? source = null)
        {
            Health += heal;
            BattleLog = $" +{heal}hp from {source} |";
        }

        #endregion

        #region Condition Methods

        public virtual void ApplyCondition(Condition condition, string? source = null)
        {
            if (IsDestroyed == false)
            {
                var existingCon = Conditions.FirstOrDefault(con => con.GetType() == condition.GetType() );

                if (existingCon != null)
                {
                    existingCon.Duration += condition.Duration;
                    existingCon.ImmediateEffect(this);
                } 
                else
                {
                    Conditions.Add(condition);
                    condition.ImmediateEffect(this);
                }

                BattleLog += $" gained {condition.Name}({condition.Duration}) from {source} |";

            }

        }

        public virtual void RoundEndConditionEffects()
        {
            foreach (var con in Conditions)
            {

                if  ( ConditionImmunities.Any(conImmune => conImmune.GetType() == con.GetType() ) )
                {
                    continue;
                }

                con.RoundEndEffect(this);

                if ( con.Duration == 0 )
                {
                    con.ExpireEffect(this);
                }

            }

            Conditions.RemoveAll(x => x.Duration <= 0);
        }

        #endregion


        public virtual void SelfDestruct()
        {
            Console.Write($"{Name} has been destroyed | ");
        }

    }

   

}
