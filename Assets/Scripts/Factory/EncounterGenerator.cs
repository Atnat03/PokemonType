using System;
using System.Collections.Generic;
using CommandPattern;
using Fights;
using Pokemons;
using UnityEngine;

namespace Factory
{
	public interface IEncounter
	{
		void StartEncounter();
	}

	[Serializable]
	public class PokemonListData
	{
		public PokemonSO data;
		public float percentageLuckSpawn;
	}
	
	public abstract class EncounterGenerator : Observer
	{
		#region Properties

		#endregion


		#region Variables

		[SerializeField] private List<PokemonListData> pokemonSpawnable;
		
		PlayerDresseur currentDresseur;
		PokemonBehavior currentPokemon;
		PokemonBehavior currentPlayerPokemon;
		
		#endregion


		#region Fonctions

		protected abstract IEncounter CreateEncounter(PokemonBehavior player, PokemonBehavior enemy);
		
		protected virtual void TriggerFight(PokemonSO firstPokemon)
		{
			if (FightManager.Instance.isFight) return;
			
			PokemonSO pokemon = GetRandomPokemon();
			
			IEncounter encounter = CreateEncounter(currentPlayerPokemon, currentPokemon);

			encounter.StartEncounter();

			InvokeEvent(new OnSwapScene{newSceneIndex = ScenesState.Fight});
			
			FightManager.Instance.InitFight(firstPokemon, pokemon, this);
		}

		PokemonSO GetRandomPokemon()
		{
			float total = 0f;

			foreach (PokemonListData item in pokemonSpawnable)
			{
				total += item.percentageLuckSpawn;
			}

			float randomValue = UnityEngine.Random.Range(0f, total);

			foreach (PokemonListData item in pokemonSpawnable)
			{
				if (randomValue < item.percentageLuckSpawn)
				{
					return item.data;
				}

				randomValue -= item.percentageLuckSpawn;
			}

			return null;
		}

		public virtual void EndFight()
		{
			if(currentPokemon != null)
				Destroy(currentPokemon.gameObject);
			
			if(currentPlayerPokemon != null)
				Destroy(currentPlayerPokemon.gameObject);
			
			InvokeEvent(new OnSwapScene{newSceneIndex = ScenesState.Default});
		}
		
		#endregion
	}
}