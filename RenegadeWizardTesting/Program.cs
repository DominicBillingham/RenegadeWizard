

var player = new Entity();
player.Health = 5;



player.Conditions.Add(new Defended());
player.Conditions.Add(new Wounded()); 
player.Conditions.Add(new Immortal());

player.TakeDamage(3);


Console.WriteLine(player.Health);
Console.WriteLine();


public class Entity
{
    public int Health { get; set; }
    public List<Condition> Conditions { get; set; } = new();

    public void TakeDamage(int damage)
    {
        foreach (var con in Conditions)
        {
            damage = con.ModifyDamage(damage);
            if (con.HasPriority)
            {
                break;
            }
        }
        Health -= damage;
    }
}


public class Condition
{
    public bool HasPriority = false;

    private DamageModifier? damgageMods;

    public int ModifyDamage(int damage)
    {
        damgageMods?.ModifyDamage(damage);
    }
}

public interface DamageModifier
{
    public int ModifyDamage(int damage);
}


public class Defended : Condition
{
    public override int ModifyDamage(int damage)
    {
        return damage -= 1;
    }

}
 
public class Wounded : Condition
{
    public override int ModifyDamage(int damage)
    {
        return damage += 1;
    }
}

public class Immortal : Condition
{
    public override int ModifyDamage(int damage)
    {
        return damage = 0;
    }
    public Immortal()
    {
        HasPriority = true;
    }
}