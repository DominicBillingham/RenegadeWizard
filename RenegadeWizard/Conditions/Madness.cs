using RenegadeWizard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Conditions
{
    class Madness : Condition
    {
        public Madness(int duration) : base(duration)
        {
            Name = "Madness";
        }
        public override void RoundEndEffect(Entity entity)
        {
            Duration -= 1;
        }

        public override void ImmediateEffect(Entity entity)
        {

        }

        public override void ExpireEffect(Entity entity)
        {

        }

    }
}
