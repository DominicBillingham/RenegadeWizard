using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    class Burning : Condition
    {
        public Burning(int duration) : base(duration)
        {
            Name = "Burning";
        }
        public override void ApplyEffect(Entity entity)
        {
            entity.ApplyConditionDamage(1, Name);
            Duration -= 1;
        }

    }
}
