using System;
using System.Collections;
using Factory;
using MyPrint;
using UnityEngine;
using Console = MyPrint.Console;
using Random = UnityEngine.Random;

namespace Fights
{
    public class FightManager : MonoBehaviour
    {
        public static FightManager Instance;
        
        public FightState currentState;

        private PokemonBehavior playerPokemon;
        private PokemonBehavior enemyPokemon;
        
        private EncounterGenerator currentEncounter;

        private void Awake()
        {
            Instance = this;
        }

        public void InitFight(PokemonBehavior player, PokemonBehavior enemy, EncounterGenerator encounter)
        {
            playerPokemon = player;
            enemyPokemon = enemy;
            currentEncounter = encounter;

            currentState = FightState.Start;
            StartCoroutine(StartFight());
        }

        private IEnumerator StartFight()
        {
            Console.Print("Le combat commence !", ColorConsole.Green);
            yield return new WaitForSeconds(1f);

            currentState = FightState.PlayerTurn;
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            Console.Print("Tour du joueur", ColorConsole.Blue);
        }

        public void OnPlayerAttack1()
        {
            if (currentState != FightState.PlayerTurn) return;

            currentState = FightState.Busy;
            StartCoroutine(PlayerAttack(() => playerPokemon.Attack1()));
        }

        public void OnPlayerAttack2()
        {
            if (currentState != FightState.PlayerTurn) return;

            currentState = FightState.Busy;
            StartCoroutine(PlayerAttack(() => playerPokemon.Attack2()));        
        }

        private IEnumerator PlayerAttack(Func<int> attack)
        {
            int damage = attack.Invoke();

            enemyPokemon.TakeDamage(damage);

            yield return new WaitForSeconds(1f);
            
            if (enemyPokemon.currentHP <= 0)
            {
                currentState = FightState.Win;
                EndFight(true);
                yield break;
            }

            currentState = FightState.EnemyTurn;
            EnemyTurn();
        }

        private void EnemyTurn()
        {
            Console.Print("Tour de l'ennemi", ColorConsole.Red);
            StartCoroutine(EnemyAttack());
        }

        private IEnumerator EnemyAttack()
        {
            yield return new WaitForSeconds(1f);

            int damage;

            int choice = Random.Range(0, 2);

            if (choice == 0)
                damage = enemyPokemon.Attack1();
            else
                damage = enemyPokemon.Attack2();

            playerPokemon.TakeDamage(damage);

            yield return new WaitForSeconds(1f);

            if (playerPokemon.currentHP <= 0)
            {
                currentState = FightState.Lose;
                EndFight(false);
                yield break;
            }

            currentState = FightState.PlayerTurn;
            PlayerTurn();
        }
        
        private void EndFight(bool isWin)
        {
            if (isWin)
                Console.Print("Victoire !", ColorConsole.Green);
            else
                Console.Print("Défaite...", ColorConsole.Red);
            
            currentEncounter.EndFight();
        }
    }
}