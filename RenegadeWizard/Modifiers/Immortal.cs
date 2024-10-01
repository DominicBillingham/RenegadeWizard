using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Immortal : Modifier
    {
        public Immortal(int duration) : base(duration)
        {
            Name = "Immortal";
            CompendiumNote = "Makes a creature completely immune to damage";
        }

        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

        public override int ModifyDamageTaken(int damage)
        {
            return 0;
        }

    }
}
