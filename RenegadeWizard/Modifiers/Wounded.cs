using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Wounded : Modifier
    {
        public Wounded(int duration) : base(duration)
        {
            Name = "Wounded";
        }

        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

        public override int ModifyDamageTaken(int damage)
        {
            damage++;
            return damage;
        }
    }
}
