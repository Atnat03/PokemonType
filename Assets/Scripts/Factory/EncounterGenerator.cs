using System;
using CommandPattern;
using Fights;
using UnityEngine;

namespace Factory
{
	public interface IEncounter
	{
		void StartEncounter();
		void Attack();
		void Heal();
		void EnemyAttack();
		void End();
	}
	
	public abstract class EncounterGenerator : MonoBehaviour
	{
		#region Properties

		#endregion


		#region Variables

		[SerializeField] private PokemonSO pokemon;
		[SerializeField] private GameObject fightUI;
		[SerializeField] public bool isFight = false;
		
		PlayerDresseur currentDresseur;
		PokemonBehavior currentPokemon;
		PokemonBehavior currentPlayerPokemon;
		
		#endregion


		#region Fonctions

		protected abstract IEncounter CreateEncounter(PokemonBehavior player, PokemonBehavior enemy);
		
		protected virtual void Awake()
		{
			fightUI.SetActive(false);
		}

		protected virtual void TriggerFight(PlayerDresseur player)
		{
			Vector3 posToLookAt = player.transform.position - Vector3.up + Vector3.forward;
			
			//PlayerPokemon
			Vector3 playerPokePos = player.transform.position - Vector3.up + Vector3.forward * 0.5f;
			currentPlayerPokemon = PokemonFactory.Instance.CreatePokemon(player.FirstPokemon.data, playerPokePos, Quaternion.identity);
			currentPlayerPokemon.transform.LookAt(posToLookAt);
			
			//EnemyPokemon
			currentPokemon = PokemonFactory.Instance.CreatePokemon(pokemon, transform.position, Quaternion.identity);
			currentPokemon.transform.LookAt(posToLookAt);
			
			fightUI.SetActive(true);
			currentDresseur = player;
			isFight = true;
			currentDresseur.StartFight(true);
			
			IEncounter encounter = CreateEncounter(currentPlayerPokemon, currentPokemon);

			encounter.StartEncounter();

			FightManager.Instance.InitFight(currentPlayerPokemon, currentPokemon, this);
		}

		public virtual void EndFight()
		{
			fightUI.SetActive(false);
			isFight = false;
			currentDresseur.StartFight(false);
			
			if(currentPokemon != null)
				Destroy(currentPokemon.gameObject);
			
			if(currentPlayerPokemon != null)
				Destroy(currentPlayerPokemon.gameObject);
		}
		
		#endregion
	}
}