using System;
using System.Collections;
using System.Collections.Generic;
using CommandPattern;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDresseur : Observer
{
	#region Properties

	public PokemonSO FirstPokemon => playerData.pokemonsTeam[0];
	
	#endregion
	
	#region Fonctions
	
	PlayerController playerController;
	PlayerData playerData;
	
	public bool wasInFight = false;
	
	[SerializeField] private PokemonFollow pokemonFollowPrefab;
	[SerializeField] private Transform spawnPointPokemon;
	private PokemonFollow currentPokemon;
	
	[SerializeField] Animator animator;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
		playerData = PlayerData.Instance;
		
		ListenToEvent<OnSwapScene>(Freeze);
	}

	public bool ThrowPokemon()
	{
		animator.SetTrigger("Throw");

		StartCoroutine(DelayDoOnPokemon());
		
		return true;
	}

	IEnumerator DelayDoOnPokemon()
	{
		yield return new WaitForSeconds(0.5f);
		
		if (currentPokemon ==null)
		{
			currentPokemon = Instantiate(pokemonFollowPrefab, spawnPointPokemon.position, spawnPointPokemon.rotation);
			currentPokemon.SetTarget(spawnPointPokemon);
		}
		else
		{
			Destroy(currentPokemon.gameObject);
		}
	}
	
	void Freeze(OnSwapScene data)
	{
		playerController.enabled = data.newSceneIndex == ScenesState.Default;
		animator.SetFloat("Moving", 0);

		if (data.newSceneIndex == ScenesState.Default)
		{
			StartCoroutine(DelayRespawn());
		}
	}

	IEnumerator DelayRespawn()
	{
		wasInFight = true;
		yield return new WaitForSeconds(1.5f);
		wasInFight = false;
	}
	
	#endregion

}
