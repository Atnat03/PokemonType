using System;
using System.Collections;
using CommandPattern;
using Factory;
using MyPrint;
using Pokemons;
using UnityEngine;
using Console = MyPrint.Console;
using Random = UnityEngine.Random;

namespace Fights
{
    [Serializable] public enum FightActions{Actions, Attack, Bag}
    
    public class FightManager : Observer
    {
        public static FightManager Instance;
        
        public FightState currentState;

        private PokemonBehavior playerPokemon;
        private PokemonBehavior enemyPokemon;
        
        private EncounterGenerator currentEncounter;

        [SerializeField] private Transform playerPokemonTransform;
        [SerializeField] private Transform ennemyPokemonTransform;
        
        public bool isFight = false;
        
        FightActions currentAction = FightActions.Actions;

        public int potionHealValue = 20;
        
        //Actions
        public Action<FightActions> OnPannelChange;
        public Action<PokemonSO, string> OnCombatStart;
        public Action<float, float> OnPokemonAttack;
        
        private void Awake()
        {
            Instance = this;
        }

        public void InitFight(PokemonSO playerPokemonStarter, PokemonSO enemy, EncounterGenerator encounter)
        {
            isFight = true;
            
            //Player Pokemon
            playerPokemon = PokemonFactory.Instance.CreatePokemon(playerPokemonStarter,
                playerPokemonTransform.position, playerPokemonTransform.rotation, playerPokemonTransform);
            
            //Ennemy Pokemon
            enemyPokemon = PokemonFactory.Instance.CreatePokemon(enemy, 
                ennemyPokemonTransform.position, ennemyPokemonTransform.rotation,ennemyPokemonTransform);
            
            currentEncounter = encounter;

            playerPokemon.SetUpHP();
            enemyPokemon.SetUpHP();

            currentState = FightState.Start;

            ChangePannel(currentAction);
            
            OnCombatStart?.Invoke(playerPokemon.data, enemyPokemon.data.Name);
            
            OnPokemonAttack?.Invoke(
                playerPokemon.currentHP / playerPokemon.data.HP,
                enemyPokemon.currentHP / enemyPokemon.data.HP);
            
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

        public void OnPlayerAttack(int attackIndex)
        {
            if (currentState != FightState.PlayerTurn) return;
            currentState = FightState.Busy;
            StartCoroutine(PlayerAttack(attackIndex));
        }
        
        private IEnumerator PlayerAttack(int attackIndex)
        {
            AttackSO attack = attackIndex switch
            {
                0 => playerPokemon.data.Attack1,
                1 => playerPokemon.data.Attack2,
                2 => playerPokemon.data.Attack3,
                3 => playerPokemon.data.Attack4,
                _ => null
            };

            if (attack == null) yield break;

            int damage = 0;
            yield return StartCoroutine(playerPokemon.PerformAttack(attack, enemyPokemon.transform, dmg => damage = dmg));

            float mult = PokemonUtils.DamageTypeMultiplier(attack.Type, enemyPokemon.data.type);
            enemyPokemon.TakeDamage((int)(damage * mult));
            
            OnPokemonAttack?.Invoke(
                playerPokemon.currentHP / playerPokemon.data.HP,
                enemyPokemon.currentHP / enemyPokemon.data.HP);
            
            yield return new WaitForSeconds(1f);

            if (enemyPokemon.currentHP <= 0) { EndFight(true); yield break; }
            currentState = FightState.EnemyTurn;
            EnemyTurn();
        }

        private IEnumerator EnemyAttack()
        {
            yield return new WaitForSeconds(1f);

            int choice = Random.Range(0, 4);
            AttackSO attack = choice switch
            {
                0 => enemyPokemon.data.Attack1,
                1 => enemyPokemon.data.Attack2,
                2 => enemyPokemon.data.Attack3,
                3 => enemyPokemon.data.Attack4,
                _ => null
            };

            if (attack == null)
            {
                            
                currentState = FightState.PlayerTurn;
                PlayerTurn();
                yield break;
            }

            int damage = 0;
            yield return StartCoroutine(enemyPokemon.PerformAttack(attack, playerPokemon.transform, dmg => damage = dmg));

            float multiplicator = PokemonUtils.DamageTypeMultiplier(attack.Type, playerPokemon.data.type);
            
            switch (multiplicator)
            {
                case 0 : Console.Print("Attack qui n'a aucun effet...", ColorConsole.Yellow); break;
                case 0.5f : Console.Print("Attack peut efficace...", ColorConsole.Yellow); break;
                case 1 : Console.Print("Attack normal...", ColorConsole.Yellow); break;
                case 2 : Console.Print("Attack super efficace !!", ColorConsole.Yellow); break;
            }
            
            playerPokemon.TakeDamage((int)(damage * multiplicator));
            
            OnPokemonAttack?.Invoke(
                playerPokemon.currentHP / playerPokemon.data.HP,
                enemyPokemon.currentHP / enemyPokemon.data.HP);
            
            yield return new WaitForSeconds(1f);

            if (playerPokemon.currentHP <= 0)
            {
                EndFight(false); 
                yield break;
            }
            
            currentState = FightState.PlayerTurn;
            PlayerTurn();
        }

        private void EnemyTurn()
        {
            Console.Print("Tour de l'ennemi", ColorConsole.Red);
            StartCoroutine(EnemyAttack());
        }
        
        void TryCatch()
        {
            if (currentState != FightState.PlayerTurn) return;
            if (enemyPokemon == null) return;

            InvokeEvent(new OnPokemonCapture {pokemon = enemyPokemon.data});
            
            EndFight(true);
        }
        
        private void EndFight(bool isWin)
        {
            if (isWin)
                Console.Print("Victoire !", ColorConsole.Green);
            else
                Console.Print("Défaite...", ColorConsole.Red);
            
            if (playerPokemon != null)
            {
                Destroy(playerPokemon.gameObject);
                playerPokemon = null;
            }

            if (enemyPokemon != null)
            {
                Destroy(enemyPokemon.gameObject);
                enemyPokemon = null;
            }
            
            currentEncounter.EndFight();
            
            InvokeEvent(new OnSwapScene{newSceneIndex = ScenesState.Default});
            
            isFight = false;
        }
        
        public void ChangePannelToActions() => ChangePannel(FightActions.Actions);
        public void ChangePannelToAttack()  => ChangePannel(FightActions.Attack);
        public void ChangePannelToBag()     => ChangePannel(FightActions.Bag);
        
        void ChangePannel(FightActions action)
        {
            if(currentState != FightState.PlayerTurn) return;

            currentAction = action;
            
            OnPannelChange?.Invoke(currentAction);
        }

        public void Fuite()
        {
            EndFight(false);
        }

        public void ThrowBall()
        {
            TryCatch();
        }

        public void UsePotion()
        {
            playerPokemon.AddHealth(potionHealValue);
            
            currentState = FightState.EnemyTurn;
            ChangePannel(FightActions.Actions);
            OnPannelChange?.Invoke(currentAction);
        }
    }
}