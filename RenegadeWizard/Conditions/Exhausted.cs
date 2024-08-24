using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Exhausted : Condition
    {
        int previousStr;
        int previousAgi;
        int previousInt;

        public Exhausted(int duration) : base(duration)
        {
            Name = "Exhausted";
        }

        public override void RoundEndEffect(Entity entity)
        {

            if (entity.Attributes != null)
            {
                previousStr = entity.Attributes.Strength;
                previousAgi = entity.Attributes.Agility;
                previousInt = entity.Attributes.Intellect;
            }

            Duration -= 1;
        }

        public override void ImmediateEffect(Entity entity)
        {


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
