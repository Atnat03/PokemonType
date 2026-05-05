using System;
using MyPrint;
using Pokemons;
using UnityEngine;
using Console = MyPrint.Console;
using Random = UnityEngine.Random;

namespace Factory
{
    public class WildEncounter : IEncounter
    {
        private PokemonBehavior player;
        private PokemonBehavior enemy;

        public WildEncounter(PokemonBehavior player, PokemonBehavior enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void StartEncounter()
        {
            Debug.Log("Rencontre sauvage !");
        }
        
        public void End()
        {
            Debug.Log("Fin rencontre sauvage");
        }
    }
}