using MyPrint;
using Pokemons;
using UnityEngine;

namespace Factory
{
    using UnityEngine;

    public class FightEncounter : IEncounter
    {
        private PokemonBehavior player;
        private PokemonBehavior enemy;

        public FightEncounter(PokemonBehavior player, PokemonBehavior enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void StartEncounter()
        {
            Debug.Log("Combat contre un dresseur !");
        }

        public void End()
        {
            Debug.Log("Fin combat dresseur");
        }
    }
}