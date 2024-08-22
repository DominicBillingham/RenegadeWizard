using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{

    public class Kobold : Creature
    {
        public Kobold(string name)
        {
            Name = name;
            Description = "Short gnome";
            Health = 3;
            CharacterActions = new Interaction();
            Attributes = new Attributes(2, 8, 2);
        }
        public override int WhenGrabbed(Entity grabber)
        {
            base.WhenGrabbed(grabber);
            grabber.ApplyCondition(new Bleeding(3), $"{Name} biting back");

            return 1;
        }

    }

}
