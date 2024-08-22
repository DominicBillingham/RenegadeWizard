using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Bleeding : Condition
    {
        public Bleeding(int duration) : base(duration)
        {
            Name = "Bleeding";
        }
        public override void ApplyEffect(Entity entity)
        {
            entity.ApplyDamage(1, Name, true);
            Duration -= 1;
        }
    }
}
