using System;
using MyPrint;
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

        public void Attack()
        {
            int damage = player.Attack1();
            enemy.TakeDamage(damage);
        }

        public void Heal()
        {
            Debug.Log("Heal joueur");
        }

        public void EnemyAttack()
        {
            int damage = enemy.Attack1();
            player.TakeDamage(damage);
        }

        public void End()
        {
            Debug.Log("Fin rencontre sauvage");
        }

        // spécifique
        public void Capture()
        {
            Debug.Log("Tentative de capture !");
        }

        public void Flee()
        {
            Debug.Log("Fuite !");
            End();
        }
    }
}