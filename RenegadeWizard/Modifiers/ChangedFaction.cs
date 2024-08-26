using RenegadeWizard.Entities;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Modifiers
{
    class ChangedFaction : Modifier
    {
        private Factions NewFaction;
        public ChangedFaction(int duration, Factions newFaction) : base(duration)
        {
            Name = "ChangedFaction";
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
