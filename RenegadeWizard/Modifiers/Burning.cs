﻿using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Modifiers
{
    class Burning : Modifier
    {
        public Burning(int duration) : base(duration)
        {
            Name = "Burning";
            CompendiumNote = "Deals 2 damage at the end of the round, and can also spread randomly";
           
        }
        public override void OnRoundEnd(Entity entity)
        {
            entity.ApplyDamage(2, Name, true);

            if (Random.Shared.Next(2) == 0)
            {
                var target = new EntQuery().SelectNotBurning().SelectCreatures().GetRandom();
                if (target != null)
                {
                    target.ApplyCondition(new Burning(2), "spreading fire");
                }
            }

            Duration -= 1;
        }

    }
}
