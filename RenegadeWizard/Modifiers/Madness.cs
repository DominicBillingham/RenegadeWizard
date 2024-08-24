using RenegadeWizard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Modifiers
{
    class Madness : Modifier
    {
        public Madness(int duration) : base(duration)
        {
            Name = "Madness";
        }
        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

    }
}
