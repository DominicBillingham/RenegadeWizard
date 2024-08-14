using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeWizard.Entities
{
    public class Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsDestroyed { get { return Health < 1; } }
        public List<Condition> Conditions { get; set; } = new List<Condition>();

        // Composition Stuff
        public Actions? Actions { get; set; }
        public Attributes? Attributes { get; set; }
        public Entity? HeldObject { get; set; }

        // WhenAction Methods
        public virtual int WhenThrown(Entity target, Entity thrower)
        {
            Console.WriteLine($" ! {thrower.Name} tries to throw {Name}, but it fails");
            return 0;
        }
        public virtual int WhenDrank(Entity drinker)
        {
            Console.WriteLine($" ! {drinker.Name} tries to drink {Name}, but it fails");
            return 0;
        }
        public virtual int WhenGrabbed(Entity grabber)
        {
            Console.WriteLine($" ! {grabber.Name} tries to grab {Name}, but it fails");
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
                Console.WriteLine($"{Name} is immortal and takes 0 damage from {source}");
                return;
            }

            // idea: Check for damage immunitiess

            if (HeldObject != null && HeldObject.IsDestroyed == false)
            {
                HeldObject.Health -= damage;
                Console.WriteLine($" # {HeldObject.Name} takes -{damage}hp from {source} to shield {Name}");

                if ( HeldObject.IsDestroyed )
                {
                    HeldObject.SelfDestruct();
                    Console.WriteLine($" # {HeldObject.Name} is destroyed!");
                }

                return;
            } 

            if (IsDestroyed == false)
            {
                Health -= damage;
                Console.Write($" # {Name} takes -{damage}hp from {source}");
                if(IsDestroyed)
                {
                    SelfDestruct();
                }
                Console.WriteLine();
            }
        }
        public virtual void ApplyConditionDamage(int damage, string? source = null)
        {
            if (Conditions.Any(con => con is Immortal))
            {
                Console.WriteLine($"{Name} is immortal and takes 0 damage from {source}");
                return;
            }

            // idea: Check for damage immunitiess
            if (IsDestroyed == false)
            {
                Health -= damage;
                Console.Write($" # {Name} takes -{damage}hp from {source}");
                if (IsDestroyed)
                {
                    Console.Write($" | {Name} has been destroyed");
                }
                Console.WriteLine();
            }
        }

        public virtual void ApplyHealing(int heal, string? source = null)
        {
            Health += heal;
            Console.WriteLine($" # {Name} recovers +{heal}hp from {source}");
        }
        public virtual void ApplyCondition(Condition condition, string? source = null)
        {
            Console.WriteLine($" # {Name} is {condition.Name} from {source} ");
            Conditions.Add(condition);
        }
        public virtual void SelfDestruct()
        {
            Console.Write($" | {Name} has been destroyed");
            Scene.Entities.Remove(this);
        }

    }

   

}
