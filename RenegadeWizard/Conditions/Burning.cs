using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Conditions
{
    class Burning : Condition
    {
        public Burning(int duration) : base(duration)
        {
            Name = "Burning";
        }
        public override void RoundEndEffect(Entity entity)
        {
            entity.ApplyDamage(2, Name, true);

            if (Random.Shared.Next(2) == 0)
            {
                var fireTarget = Scene.GetFireSpreadTarget(entity);
                fireTarget.ApplyCondition(new Burning(2));
                Console.Write($" The fire spreads | ");
            }

            Duration -= 1;
        }
        public override void ImmediateEffect(Entity entity)
        {

        }

        public override void ExpireEffect(Entity entity)
        {

        }

    }
}
