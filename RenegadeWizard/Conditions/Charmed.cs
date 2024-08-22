using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Charmed : Condition
    {
        public Charmed(int duration) : base(duration)
        {
            Name = "Charmed";
        }
        public override void ApplyEffect(Entity entity)
        {
            Duration -= 1;
        }

    }
}
