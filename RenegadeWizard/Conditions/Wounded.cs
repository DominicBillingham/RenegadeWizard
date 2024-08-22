using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Wounded : Condition
    {
        public Wounded(int duration) : base(duration)
        {
            Name = "Wounded";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;
        }

    }
}
