using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class Item : Entity
    {
        public override int WhenGrabbed(Entity grabber)
        {
            if (Conditions.Any(con => con is Slippery))
            {
                Console.WriteLine($" # {Name} too slippery!");
                return 0;
            }

            if (Conditions.Any(con => con is Burning))
            {
                grabber.ApplyCondition(new Burning(3), $"trying to grab {Name}");
            }

            grabber.HeldObject = this;
            Console.WriteLine($" # {Name} is being used as a shield by {grabber.Name}");
            return 1;
        }
    }

}
