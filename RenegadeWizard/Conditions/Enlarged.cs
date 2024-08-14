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
            entity.Attributes.Strength = 20;
            Duration -= 1;
        }

    }
}
