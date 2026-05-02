using MyPrint;
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
            Debug.Log("Fin combat dresseur");
        }

        // spécifique
        public void Taunt()
        {
            Debug.Log("Provocation !");
        }
    }
}