using CommandPattern;
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

        public PokemonBehavior CreatePokemon(PokemonSO data, Vector3 position, Quaternion rotation)
        {
            GameObject obj = Instantiate(data.prefab, position, rotation).gameObject;

            PokemonBehavior pokemon = obj.GetComponent<PokemonBehavior>();
            pokemon.data = data;

            return pokemon;
        }
    }
}