using RenegadeWizard.Entities;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Modifiers
{
    class Charmed : Modifier
    {
        private Factions NewFaction;
        public Charmed(int duration, Factions newFaction) : base(duration)
        {
            Name = "Charmed";
            NewFaction = newFaction;
        }

        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

        public override Factions OverwriteFaction()
        {
            return NewFaction;
        }

    }
}
