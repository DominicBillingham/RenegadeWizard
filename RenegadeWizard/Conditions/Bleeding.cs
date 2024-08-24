using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Bleeding : Condition
    {
        public Bleeding(int duration) : base(duration)
        {
            Name = "Bleeding";
        }

        public override void RoundEndEffect(Entity entity)
        {
            entity.ApplyDamage(1, Name, true);
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
