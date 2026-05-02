using System;
using MyPrint;
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

        protected override void Awake()
        {
            base.Awake();

            grassBlock = GetComponent<GrassBlock>();
            collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            collider.size = new Vector3(grassBlock.Size.x, 0.25f, grassBlock.Size.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isFight) return;

            if (other.TryGetComponent<PlayerDresseur>(out PlayerDresseur dresseur))
            {
                float n = Random.Range(0, 100);
                bool isTrigger = n < probabilityTrigger;

                if (isTrigger)
                {
                    TriggerFight(dresseur);
                    Console.Print("Rencontre sauvage !", ColorConsole.Green);
                }
            }
        }

        protected override IEncounter CreateEncounter(PokemonBehavior player, PokemonBehavior enemy)
        {
            return new WildEncounter(player, enemy);
        }

        protected override void TriggerFight(PlayerDresseur player)
        {
            base.TriggerFight(player);

            grassBlock.EnableGrass(false);
        }

        public override void EndFight()
        {
            base.EndFight();

            grassBlock.EnableGrass(true);
        }
    }
}