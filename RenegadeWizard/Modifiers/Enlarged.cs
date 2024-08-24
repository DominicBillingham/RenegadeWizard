using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Enlarged : Modifier
    {

        public Enlarged(int duration) : base(duration)
        {
            Name = "Enlarged";
        }

        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

        public override int ModifyStrength(int strength)
        {
            strength *= 2;
            return strength;
        }


    }
}
