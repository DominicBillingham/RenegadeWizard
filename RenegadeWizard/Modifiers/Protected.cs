using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Protected : Modifier
    {
        public Protected(int duration) : base(duration)
        {
            Name = "Protected";
        }
        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }
        public override int ModifyDamageTaken(int damage)
        {
            damage--;
            return damage;
        }

    }
}

