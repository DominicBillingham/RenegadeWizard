
Console.WriteLine("Hello!");
Entity player = new();

player.Conditions.Add(new Wounded());
player.Conditions.Add(new Burning());

player.TakeDamage(5);
Console.WriteLine(player.Health);

AdjectiveHelper.ApplyRoundEndEffects(player);
Console.WriteLine(player.Health);


public static class AdjectiveHelper
{

    public static void ApplyRoundEndEffects(Entity entity)
    {
        foreach (var con in entity.Conditions)
        {
            con.OnRoundEnd(entity);
        }
    }

    public static int ModifyDamageTaken(Entity entity, int damage)
    {
        foreach (var con in entity.Conditions)
        {
            damage = con.ModifyIncomingDamage(damage);
        }
        return damage;
    }


}

public abstract class Condition
{
    public virtual void OnApplication(Entity entity) { }

    public virtual void OnRoundEnd(Entity entity) { }

    public virtual void OnContact(Entity entity) { }

    public virtual int ModifyIncomingDamage(int damage) { return damage; }
}



public class Burning : Condition
{


    public override void OnRoundEnd(Entity entity)
    {
        entity.TakeDamage(2);
    }

    public override void OnContact(Entity entity)
    {
        Console.WriteLine("Burns spreads to other creature");
    }



}

public class Wounded : Condition
{
    public override int ModifyIncomingDamage(int damage)
    {
        damage++;
        return damage;
    }

}




public class Entity
{
    public int Health { get; set; } = 10;
    public int Strength { get; set; } = 10;
    public List<Condition> Conditions { get; set; } = new();

    public void TakeDamage(int damage)
    {
        Health -= AdjectiveHelper.ModifyDamageTaken(this, damage);
    }
}
