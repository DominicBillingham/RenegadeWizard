using RenegadeWizard.Entities;

namespace RenegadeWizard.Conditions
{
    public abstract class Condition
    {
        public int Duration { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public Condition(int duration)
        {
            Duration = duration;
        }
        abstract public void ApplyEffect(Entity entity);

    }

}
