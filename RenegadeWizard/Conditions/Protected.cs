using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Protected : Condition
    {
        public Protected(int duration) : base(duration)
        {
            Name = "Protected";
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

