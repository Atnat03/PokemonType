using System;
using System.Collections;
using System.Collections.Generic;
using CommandPattern;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDresseur : MonoBehaviour
{
	#region Properties

	public PokemonBehavior FirstPokemon => pokemonsTeam[0].prefab;
	
	#endregion


	#region Variables

	#endregion


	#region Fonctions
	
	PlayerController playerController;
	private bool isInFight = false;
	
	[SerializeField] private PokemonFollow pokemonFollowPrefab;
	[SerializeField] private Transform spawnPointPokemon;
	private PokemonFollow currentPokemon;
	
	[SerializeField] Animator animator;
	
	[Header("Pokemons")]
	[SerializeField] private List<PokemonSO> pokemonsTeam;
	[SerializeField] private PokemonSO staterPokemon;
	
	//Actions
	public Action<List<PokemonSO>> OnTeamUpdate;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}

	private void Start()
	{
		AddPokemonInTeam(staterPokemon);
	}

	public bool ThrowPokemon()
	{
		if (isInFight) return false;
		
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
	
	#endregion

	public void StartFight(bool state)
	{
		playerController.enabled = !state;
		animator.SetFloat("Moving", 0);
		isInFight = state;
	}

	#region Team

	public void AddPokemon() => AddPokemonInTeam(staterPokemon);
	
	public void AddPokemonInTeam(PokemonSO pokemon)
	{ 
		pokemonsTeam.Add(pokemon);
		OnTeamUpdate?.Invoke(pokemonsTeam);
	}
	public void RemovePokemonFromTeam(PokemonSO pokemon)
	{ 
		pokemonsTeam.Remove(pokemon);
		OnTeamUpdate?.Invoke(pokemonsTeam);
	}
	public void SetFirstPokemonInTeam(PokemonSO pokemon)
	{ 
		pokemonsTeam[0] = pokemon;
		OnTeamUpdate?.Invoke(pokemonsTeam);
	}
	

	#endregion
}
