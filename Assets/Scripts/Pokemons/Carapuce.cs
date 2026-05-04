using MyPrint;

public class Carapuce : PokemonBehavior
{
    protected override void Awake()
    {
        base.Awake();
        Console.Print("Carapuce Awake : " + currentHP, ColorConsole.Yellow);
    }

    public override int Attack1()
    {
        Console.Print("Carapuce lance : " + data.Attack1.Name, ColorConsole.Pink);
        return data.Attack1.Damage;
    }
	
    public override int Attack2()
    {
        Console.Print("Carapuce lance : " + data.Attack2.Name, ColorConsole.Pink);
        return data.Attack2.Damage;
    }
}
