using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Enlarged : Condition
    {

        public Enlarged(int duration) : base(duration)
        {
            Name = "Enlarged";
        }

        public override void RoundEndEffect(Entity entity)
        {
            Duration -= 1;
        }

        public override void ImmediateEffect(Entity entity)
        {
            if (entity.Attributes != null)
            {
                entity.Attributes.Strength = entity.Attributes.Strength * 2;
            }
        }

        public override void ExpireEffect(Entity entity)
        {
            if (entity.Attributes != null)
            {
                entity.Attributes.Strength = entity.Attributes.Strength / 2;
            }
        }


    }
}
