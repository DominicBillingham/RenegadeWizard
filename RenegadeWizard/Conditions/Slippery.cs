using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Slippery : Condition
    {
        public Slippery(int duration) : base(duration)
        {
            Name = "Slippery";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;
        }

    }
}
