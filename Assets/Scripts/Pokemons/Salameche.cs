using MyPrint;

public class Salameche : PokemonBehavior
{
    protected override void Awake()
    {
        base.Awake();
        Console.Print("Salameche Awake : " + currentHP, ColorConsole.Yellow);
    }

    public override int Attack1()
    {
        Console.Print("Salameche lance : " + data.Attack1.Name, ColorConsole.Pink);
        return data.Attack1.Damage;
    }
	
    public override int Attack2()
    {
        Console.Print("Salameche lance : " + data.Attack2.Name, ColorConsole.Pink);
        return data.Attack2.Damage;
    }
}
