using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Enlarged : Condition
    {
        public Enlarged(int duration) : base(duration)
        {
            Name = "Enlarged";
        }
        public override void ApplyEffect(Entity entity)
        {
            entity.Attributes.Strength += 10;
            Duration -= 1;

            if (Duration <= 0)
            {
                EndEffect(entity);
            }

        }

        public void EndEffect(Entity entity)
        {
            entity.Attributes.Strength = -10;
        }


    }
}
