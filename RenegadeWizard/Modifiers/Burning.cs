using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Modifiers
{
    class Burning : Modifier
    {
        public Burning(int duration) : base(duration)
        {
            Name = "Burning";
        }
        public override void OnRoundEnd(Entity entity)
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

    }
}
