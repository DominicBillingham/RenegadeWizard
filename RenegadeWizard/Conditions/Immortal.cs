using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Immortal : Condition
    {
        public Immortal(int duration) : base(duration)
        {
            Name = "Immortal";
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
