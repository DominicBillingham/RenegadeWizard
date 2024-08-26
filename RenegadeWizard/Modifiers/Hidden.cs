using RenegadeWizard.Entities;

namespace RenegadeWizard.Modifiers
{
    class Hidden : Modifier
    {
        public Hidden(int duration) : base(duration)
        {
            Name = "Hidden";
        }

        public override void OnRoundEnd(Entity entity)
        {
            Duration -= 1;
        }

        public override Entity? ModifyTarget(Entity entity) 
        {
            var random = new EntQuery().SelectCreatures().SelectNotEntity(entity).GetRandom();
            return random;
        }

    }
}
