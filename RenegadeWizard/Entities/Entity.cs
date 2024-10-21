using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Enums;
using RenegadeWizard.GameClasses;
using static System.Net.Mime.MediaTypeNames;
using RenegadeWizard.Entities.Creatures;
using System.Reflection;

namespace RenegadeWizard.Entities
{
    public class Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; } = 1;
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
        public Attributes? Attributes { get; set; }


        #region WhenMethods
        public virtual void WhenDamaged(Interaction? trigger = null)
        {

        }

        public virtual void WhenHealed(Interaction? trigger = null)
        {

        }

        public virtual void TakeTurn()
        {
            if (IsDestroyed) { return; }
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

        #endregion

        #region Health Methods
        public virtual void ApplyDamage(int damage, string source, bool ignoreArmour = false)
        {

            if (IsDestroyed == false)
            {
                DamageTakenLastRound += damage;
                Health -= damage;
                BattleLog += $" -{damage}hp from {source} |";

            }
        }

        public virtual void ApplyHealing(int heal, string source)
        {
            if (IsDestroyed == false)
            {
                Health += heal;
                BattleLog = $" +{heal}hp from {source} |";
                HealingLastRound += heal;
            }
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
                } 
                else
                {
                    Modifiers.Add(condition);                
                }

                BattleLog += $" gained {condition.Name}({condition.Duration}) from {source} |";

            }

        }

        public void ApplyRoundEndEffects()
        {

            foreach (var con in Modifiers)
            {
                con.OnRoundEnd(this);
            }

            foreach (var con in Modifiers.Where(x => x.Duration == 0))
            {
                con.OnExpiration(this);
            }

            Modifiers.RemoveAll(con => con.Duration == 0);


        }

        #endregion

        public virtual void DeathMessage()
        {
            Console.Write($" - {Name} has been destroyed");
        }

        public virtual void WhenHearing()
        {



            if (PlayerInput.InputContains("hello"))
            {
                Console.WriteLine($" @ {Name}: HOOOOOOOOOOOOOOONNNNNNNKK, I can speak human! Finally all that duolingoing has paid off. Got [bread]");
                return;
            }

            if (PlayerInput.InputContains("honk"))
            {
                Console.WriteLine($" @ {Name}: HONK HONK [HELLO]");
                return;
            }

            if (PlayerInput.InputContains("fuck"))
            {
                Console.WriteLine($" @ {Name}: swearing is immature");
                return;
            }

            if (PlayerInput.InputContains("bread"))
            {
                Console.WriteLine($" @ {Name}: love lakes, love bread, ate' knees simple as");
                return;
            }

            Console.WriteLine($" @ {Name}: [HONK]");

        }

    }

   

}
