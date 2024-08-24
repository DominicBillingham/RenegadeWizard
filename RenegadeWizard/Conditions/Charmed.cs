using RenegadeWizard.Entities;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Conditions
{
    class Charmed : Condition
    {
        private Factions PreviousFaction;
        private Factions NewFaction;

        public Charmed(int duration, Factions previousFaction, Factions newFaction) : base(duration)
        {
            Name = "Charmed";
            PreviousFaction = previousFaction;
            NewFaction = newFaction;
        }

        public override void RoundEndEffect(Entity entity)
        {
            Duration -= 1;
        }

        public override void ImmediateEffect(Entity entity)
        {
            entity.Faction = NewFaction;
        }

        public override void ExpireEffect(Entity entity)
        {
            entity.Faction = PreviousFaction;
        }

    }
}
