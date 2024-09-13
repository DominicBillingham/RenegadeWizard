using RenegadeWizard.Entities;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Modifiers
{
    public abstract class Modifier
    {
        public int Duration { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public Modifier(int duration)
        {
            Duration = duration;
        }

        // Sometimes these methods will be called and do nothing.
        // I could use composition to fix but then I'd need to have functionality split into components
        virtual public void OnRoundEnd(Entity entity) { }
        virtual public void OnExpiration(Entity entity) { }

        virtual public void OnAttackedBy(Entity entity) { }






        virtual public int ModifyDamageTaken(int damage) { return damage; }
        virtual public int ModifyStrength(int strength) { return strength; }
        virtual public int ModifyAgility(int agility) { return agility; }
        virtual public int ModifyIntellect(int intellect) { return intellect; }



    }

}
