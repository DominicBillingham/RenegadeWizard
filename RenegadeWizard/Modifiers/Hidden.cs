using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;

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
            Console.Write($" {Narrator.GetContrastWord()} {entity.Name} is hidden, and the attack goes wide!");
            return random;
        }

    }
}
