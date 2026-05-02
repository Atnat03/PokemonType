using System;
using System.Collections;
using CommandPattern;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDresseur : MonoBehaviour
{
	#region Properties

	public PokemonBehavior FirstPokemon => firstPokemon.prefab;
	
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
	
	[Header("Test")]
	[SerializeField] private PokemonSO firstPokemon;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
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
}
