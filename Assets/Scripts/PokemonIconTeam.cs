using CommandPattern;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonIconTeam : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private TextMeshProUGUI name;

    public void SetPokemon(PokemonSO pokemon)
    {
        if (logo == null) return;
        
        logo.sprite = pokemon.icon;
        name.text = pokemon.name;
    }
}
