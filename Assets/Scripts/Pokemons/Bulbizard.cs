using MyPrint;

public class Bulbizard : PokemonBehavior
{
    protected override void Awake()
    {
        base.Awake();
        Console.Print("Bulbizard Awake : " + currentHP, ColorConsole.Yellow);
    }

    public override int Attack1()
    {
        Console.Print("Bulbizard lance : " + data.Attack1.Name, ColorConsole.Pink);
        return data.Attack1.Damage;
    }
	
    public override int Attack2()
    {
        Console.Print("Bulbizard lance : " + data.Attack2.Name, ColorConsole.Pink);
        return data.Attack2.Damage;
    }
}
