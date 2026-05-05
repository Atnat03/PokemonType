using System;
using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class PlayerData : Observer
{
    public static PlayerData Instance;
    
    [Header("Pokemons")]
    [SerializeField] public List<PokemonSO> pokemonsTeam;
    [SerializeField] private PokemonSO staterPokemon;
    
    //Actions
    public Action<List<PokemonSO>> OnTeamUpdate;
    
    void Awake()
    { 
        Instance = this;
        
        ListenToEvent<OnPokemonCapture>(AddPokemonEvent);
    }

    private void Start()
    {
        InvokeEvent(new OnPokemonCapture{pokemon = staterPokemon});
    }
    
    void AddPokemonEvent(OnPokemonCapture data) => AddPokemonInTeam(data.pokemon);
	
    void AddPokemonInTeam(PokemonSO pokemon)
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
}
