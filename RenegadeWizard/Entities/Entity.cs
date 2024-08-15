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
        public Factions Faction { get; set; } = Factions.None;
        public bool IsDestroyed { get { return Health < 1; } }
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        // Composition Stuff
        public Actions? Actions { get; set; }
        public Attributes? Attributes { get; set; }
        public Entity? HeldObject { get; set; }

        // WhenAction Methods
        public virtual int WhenThrown(Entity target, Entity thrower)
        {
            Console.Write($"{thrower.Name} tries to throw {Name}, but it fails | ");
            return 0;
        }
        public virtual int WhenConsumed(Entity consumer)
        {
            Console.Write($"{consumer.Name} tries to consume {Name}, but it fails | ");
            return 0;
        }
        public virtual int WhenGrabbed(Entity grabber)
        {
            Console.Write($"{grabber.Name} tries to grab {Name}, but it fails | ");
            return 0;
        }
        public virtual int WhenKicked(Entity kicker)
        {
            Console.Write($"{kicker.Name} tries to kick {Name}, but it fails | ");
            return 0;
        }
        public virtual int WhenInspected()
        {
            // idea: Make intellect requirements to get more details
            Console.Write($" # {Name} - {Description}");
            if ( Attributes != null)
            {
                Console.Write($" | STR:{Attributes.Strength}, AGI:{Attributes.Agility}, INT:{Attributes.Intellect}");
            }
            Console.WriteLine();
            return 0;
        }

        // Generic Entity Methods
        public virtual void ApplyDamage(int damage, string? source = null)
        {
            if (Conditions.Any(con => con is Immortal))
            {
                Console.Write($"{Name} is immortal and takes 0 damage from {source} | ");
                return;
            }

            if (HeldObject != null && HeldObject.IsDestroyed == false)
            {
                HeldObject.Health -= damage;
                Console.Write($"{HeldObject.Name} takes -{damage}hp from {source} shielding {Name} | ");

                if ( HeldObject.IsDestroyed )
                {
                    HeldObject.SelfDestruct();
                }

                Console.Write($"{Name} drops {HeldObject.Name} | ");
                HeldObject = null;

                return;
            } 

            if (IsDestroyed == false)
            {
                Health -= damage;
                Console.Write($"{Name} takes -{damage}hp from {source} | ");
                if(IsDestroyed)
                {
                    SelfDestruct();
                }
            }
        }
        public virtual void ApplyConditionDamage(int damage, string? source = null)
        {
            if (Conditions.Any(con => con is Immortal))
            {
                //Console.Write($"{Name} is immortal and takes 0 damage from {source} | ");
                return;
            }

            // idea: Check for damage immunitiess
            if (IsDestroyed == false)
            {
                Health -= damage;
                //Console.Write($"{Name} takes -{damage}hp from {source} | ");
                if (IsDestroyed)
                {
                    //Console.Write($"{Name} has been destroyed | ");
                }
            }
        }
        public virtual void ApplyHealing(int heal, string? source = null)
        {
            Health += heal;

            Conditions.RemoveAll(con => con is Bleeding);

            Console.Write($"{Name} recovers +{heal}hp from {source} | ");
        }
        public virtual void ApplyCondition(Condition condition, string? source = null)
        {
            if (IsDestroyed == false)
            {
                Console.Write($"{Name} is {condition.Name} from {source} | ");
                Conditions.Add(condition);
            }

        }
        public virtual void SelfDestruct()
        {
            Console.Write($"{Name} has been destroyed | ");
            Scene.Entities.Remove(this);
        }

    }

   

}
