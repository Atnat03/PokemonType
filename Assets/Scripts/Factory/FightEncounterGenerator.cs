using MyPrint;
using Pokemons;
using UnityEngine;
using Console = MyPrint.Console;

namespace Factory
{
    public class FightEncounterGenerator : EncounterGenerator
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerDresseur>(out PlayerDresseur dresseur))
            {
                TriggerFight(dresseur.FirstPokemon);
                
                Console.Print("Combat contre un dresseur !", ColorConsole.Red);
            }
        }

        protected override IEncounter CreateEncounter(PokemonBehavior player, PokemonBehavior enemy)
        {
            return new FightEncounter(player, enemy);
        }
    }
}