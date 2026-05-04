using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

public class PlayerDresseurView : MonoBehaviour
{
    [SerializeField] private PlayerDresseur playerDresseur;

    [Header("Team UI")] 
    [SerializeField] private PokemonIconTeam soloPokemonUI;
    [SerializeField] private Transform teamParent;

    void OnEnable()
    {
        playerDresseur.OnTeamUpdate += UpdateUI;
    }

    void OnDisable()
    {
        playerDresseur.OnTeamUpdate -= UpdateUI;
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
