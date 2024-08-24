using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Exhausted : Modifier
    {
        public Exhausted(int duration) : base(duration)
        {
            Name = "Exhausted";
        }

        public override int ModifyStrength(int strength)
        {
            strength -= Duration;
            return strength;
        }

        public override int ModifyAgility(int agility)
        {
            agility -= Duration;
            return agility;
        }

        public override int ModifyIntellect(int intellect)
        {
            intellect -= Duration;
            return intellect;
        }



    }
}
