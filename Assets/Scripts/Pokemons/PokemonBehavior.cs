using System;
using System.Collections;
using CommandPattern;
using MyPrint;
using TMPro;
using UnityEngine;
using Console = MyPrint.Console;

namespace Pokemons
{
	public class PokemonBehavior : MonoBehaviour
	{
		#region Properties

		#endregion


		#region Variables
		
		public PokemonSO data;
		public float currentHP = 100;
		
		Animator animator;
		
		#endregion


		#region Fonctions

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public IEnumerator PerformAttack(AttackSO attack, Transform target, Action<int> onDone)
		{
			if (animator != null)
			{
				string triggerName = attack.AnimationTrigger == AnimationAttackType.Throw ? "Throw" : "Charge";

				animator.SetTrigger(triggerName);
			}
			
			yield return new WaitForSeconds(attack.vfxDelay);

			if (attack.vfxPrefab != null)
				Instantiate(attack.vfxPrefab, target.position + attack.vfxOffset, target.rotation);

			yield return new WaitForSeconds(attack.postDelay);
			onDone?.Invoke(attack.Damage);
		}
		
		public void TakeDamage(int damage)
		{
			currentHP -= Mathf.Clamp(damage, 0, 100);
		
			Console.Print($"HP restant : {currentHP}", ColorConsole.Yellow);
		}

		public void SetUpHP()
		{
			currentHP = data.HP;
		}

		public void AddHealth(int health)
		{
			currentHP += Mathf.Clamp(health, 0, 100);
		}
		
		#endregion
	}

}
