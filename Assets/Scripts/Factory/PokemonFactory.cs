using CommandPattern;
using Pokemons;
using UnityEngine;

namespace Factory
{
    public class PokemonFactory : MonoBehaviour
    {
        public static PokemonFactory Instance;

        private void Awake()
        {
            Instance = this;
        }

        public PokemonBehavior CreatePokemon(PokemonSO data, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject obj = Instantiate(data.prefab, position, rotation).gameObject;

            if(parent != null)
                obj.transform.SetParent(parent);
            
            PokemonBehavior pokemon = obj.GetComponent<PokemonBehavior>();
            pokemon.data = data;

            return pokemon;
        }
    }
}