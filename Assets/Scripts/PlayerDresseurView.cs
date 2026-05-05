using System;
using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

public class PlayerDresseurView : MonoBehaviour
{
    private PlayerData playerData;

    [Header("Team UI")] 
    [SerializeField] private PokemonIconTeam soloPokemonUI;
    [SerializeField] private Transform teamParent;
    
    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

    void OnEnable()
    {
        playerData.OnTeamUpdate += UpdateUI;
    }

    void OnDisable()
    {
        playerData.OnTeamUpdate -= UpdateUI;
    }
    
    private void UpdateUI(List<PokemonSO> team)
    {
        foreach (Transform child in teamParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (PokemonSO pokemon in team)
        {
            PokemonIconTeam newPokemon = Instantiate(soloPokemonUI, teamParent);
            newPokemon.SetPokemon(pokemon);
        }
    }
}
