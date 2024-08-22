using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Rotting : Condition
    {
        public Rotting(int duration) : base(duration)
        {
            Name = "Rotting";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;

        }

    }
}
