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

        // Sometimes these methods will be called and do nothing.
        // I could use composition to fix this but it'd require nullchecks
        abstract public void ImmediateEffect(Entity entity);
        abstract public void RoundEndEffect(Entity entity);
        abstract public void ExpireEffect(Entity entity);

    }

}
