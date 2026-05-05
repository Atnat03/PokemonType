using System;
using System.Collections;
using CommandPattern;
using MyPrint;
using Pokemons;
using UnityEngine;
using Console = MyPrint.Console;
using Random = UnityEngine.Random;

namespace Factory
{
    public class WildEncounterGenerator : EncounterGenerator
    {
        [Header("Trigger")]
        [SerializeField] private GrassBlock grassBlock;
        private BoxCollider collider;

        [Header("ProbaTrigger")] 
        [SerializeField, Range(0, 100)] private float probabilityTrigger = 50;
        
        private bool isCooldown = false;

        protected void Awake()
        {
            grassBlock = GetComponent<GrassBlock>();
            collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            collider.size = new Vector3(grassBlock.Size.x * 0.75f, 0.25f, grassBlock.Size.y * 0.75f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerDresseur>(out PlayerDresseur dresseur))
            {
                if (dresseur.wasInFight) return;
                
                float n = Random.Range(0, 100);
                bool isTrigger = n < probabilityTrigger;

                if (isTrigger)
                {
                    TriggerFight(dresseur.FirstPokemon);
                    
                    Console.Print("Rencontre sauvage !", ColorConsole.Green);
                }
            }
        }

        protected override IEncounter CreateEncounter(PokemonBehavior player, PokemonBehavior enemy)
        {
            return new WildEncounter(player, enemy);
        }

        protected override void TriggerFight(PokemonSO pokemon)
        {
            base.TriggerFight(pokemon);

            grassBlock.EnableGrass(false);
        }

        public override void EndFight()
        {
            base.EndFight();
            grassBlock.EnableGrass(true);
        }
    }
}