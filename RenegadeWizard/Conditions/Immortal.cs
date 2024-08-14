using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Immortal : Condition
    {
        public Immortal(int duration) : base(duration)
        {
            Name = "Immortal";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;
        }

    }
}
