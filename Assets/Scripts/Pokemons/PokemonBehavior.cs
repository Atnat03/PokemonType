using System;
using CommandPattern;
using MyPrint;
using TMPro;
using UnityEngine;
using Console = MyPrint.Console;

public class PokemonBehavior : MonoBehaviour
{
	#region Properties

	#endregion


	#region Variables
	
	public PokemonSO data;
	public int currentHP;

	[Header("View")]
	[SerializeField] private TextMeshProUGUI textHP;
	
	#endregion


	#region Fonctions
	
	protected virtual void Awake()
	{
		currentHP = data.HP;
		textHP.text = currentHP.ToString();
	}

	public virtual int Attack1()
	{
		Console.Print("Attack 1 : " + data.Attack1.Name, ColorConsole.Pink);
		return data.Attack1.Damage;
	}
	
	public virtual int Attack2()
	{
		Console.Print("Attack 2 : " + data.Attack2.Name, ColorConsole.Pink);
		return data.Attack2.Damage;
	}
	
	public void TakeDamage(int damage)
	{
		currentHP -= damage;
	
		textHP.text = currentHP.ToString();
		
		Console.Print($"HP restant : {currentHP}", ColorConsole.Yellow);
	}
	
	#endregion
}
