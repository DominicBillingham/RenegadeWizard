using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Protected : Condition
    {
        public Protected(int duration) : base(duration)
        {
            Name = "Protected";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;
        }

    }
}
