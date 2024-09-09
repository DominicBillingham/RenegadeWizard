// See https://aka.ms/new-console-template for more information
using RulesEngineTest;
using static System.Formats.Asn1.AsnWriter;


var player = new Entity();

var demon = new Entity();
demon.Immunity = "Burning";




var fireball = new Action();
fireball.Target = demon;
fireball.Agent = player;



Console.WriteLine("Does fireball work?");

fireball.CheckImmunity("Burning");

Console.WriteLine(fireball.IsActionPossible);



class Action
{

    public Entity Agent { get; set; }
    public Entity Target { get; set; }
    public bool IsActionPossible {  get; set; } = true;
    public int ActionDamage { get; set; }

    public Action CheckImmunity(string immunity)
    {
        if (Target.Immunity == immunity)
        {
            IsActionPossible = false;
        }
        return this;
    }

    public Action ApplyDamage(int damage)
    {
        Target.
    }




    public Action 


}

// 1. Action is created, targets and agent is assigned.
// 2. action 









