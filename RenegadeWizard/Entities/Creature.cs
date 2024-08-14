using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
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
                Console.WriteLine($" ! {Name} too slippery!");
                return 0;
            }

            if (Conditions.Any(con => con is Burning))
            {
                grabber.ApplyCondition(new Burning(3), $"trying to grab Burning {Name}");
            }

            grabber.HeldObject = this;
            Console.WriteLine($"{Name} is being used as a shield by {grabber.Name} | ");
            return 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            ApplyDamage(1, "being thrown");
            target.ApplyDamage(2, $"thrown {Name}");
            return 1;
        }
    }



}
