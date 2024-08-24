using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Bleeding : Modifier
    {
        public Bleeding(int duration) : base(duration)
        {
            Name = "Bleeding";
        }

        public override void OnRoundEnd(Entity entity)
        {
            entity.ApplyDamage(1, Name, true);
        }

    }
}
