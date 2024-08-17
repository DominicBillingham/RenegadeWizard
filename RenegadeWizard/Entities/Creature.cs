using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities
{
    public class Creature : Entity
    {
        public override int WhenGrabbed(Entity grabber)
        {
            if (Conditions.Any(con => con is Slippery))
            {
                Console.Write($"{Name} is too slippery to be grabbed! | ");
                return 0;
            }

            if (grabber.Attributes?.Strength < Attributes?.Agility)
            {
                Console.Write($"{Name} is too fast to be grabbed! | ");
                return 0;
            }

            if (grabber.Attributes?.Strength < Weight)
            {
                Console.Write($"{Name} is too heavy to be grabbed! | ");
                return 0;
            }

            if (Conditions.Any(con => con is Burning))
            {
                grabber.ApplyCondition(new Burning(3), $"trying to grab Burning {Name} | ");
            }

            grabber.HeldObject = this;
            Console.Write($"{Name} is being used as a shield by {grabber.Name} | ");
            return 1;
        }

        public override int WhenConsumed(Entity consumer)
        {
            if (consumer.Attributes?.Strength < Weight)
            {
                Console.Write($"{Name} is too large to be eaten! | ");
                return 0;
            }

            Console.Write($"{Name} is devoured by {consumer.Name}! | ");
            SelfDestruct();
            return 1;

        }

        public override int WhenThrown(Entity target, Entity thrower)
        {
            ApplyDamage(1, "being thrown");
            target.ApplyDamage(2, $"thrown {Name}");
            return 1;
        }
        public override int WhenKicked(Entity kicker)
        {

            if (kicker.Attributes?.Strength > Attributes?.Strength)
            {
                ApplyDamage(2, $"being kicked by {kicker.Name}");
                Scene.GetRandomItem().ApplyDamage(2, $"{Name} crashing into it");
            } else
            {
                Console.Write($" but is too weak!");
            }

            return 1;
        }

        public override void SelfDestruct()
        {
            Console.Write($"{Name} has died | ");
            Scene.Entities.Remove(this);
        }

    }



}
