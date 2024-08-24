using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Wounded : Condition
    {
        public Wounded(int duration) : base(duration)
        {
            Name = "Wounded";
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
