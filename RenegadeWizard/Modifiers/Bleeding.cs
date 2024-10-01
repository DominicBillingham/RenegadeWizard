using RenegadeWizard.Entities;
using System.ComponentModel;

namespace RenegadeWizard.Modifiers
{
    class Bleeding : Modifier
    {
        public Bleeding(int duration) : base(duration)
        {
            Name = "Bleeding";
            CompendiumNote = "Applies 1 armour-piercing damage at the end of the round";
        }

        public override void OnRoundEnd(Entity entity)
        {
            entity.ApplyDamage(1, Name, true);
        }

    }
}
