using System;
using CommandPattern;
using MyPrint;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Console = MyPrint.Console;

namespace Fights
{
    public class FightUIManager : MonoBehaviour
    {
        [Header("References")]
        public FightManager fightManager;

        [Header("Currents Pokemons")] 
        public Image playerPokemonHpImage;
        public Image enemyPokemonHpImage;
        public TextMeshProUGUI playerPokemonNameText;
        public TextMeshProUGUI enemyPokemonNameText;
        public float speedTargetFill = 5;
        
        private float targetPlayerHP;
        private float targetEnemyHP;
        
        [Header("Pannels")]
        public GameObject[] pannelActions;

        [Header("Attacks")] 
        public ButtonAttack[] attacksButton;

        [Serializable]
        public struct ButtonAttack
        {
            public Image Image;
            public TextMeshProUGUI Text;
        }
        
        private void OnEnable()
        {
            fightManager.OnPannelChange += PannelChange;
            fightManager.OnCombatStart += SetUI;
            fightManager.OnPokemonAttack += OnUpdateHP;
        }
        
        private void OnDisable()
        {
            fightManager.OnPannelChange -= PannelChange;
            fightManager.OnCombatStart -= SetUI;
            fightManager.OnPokemonAttack -= OnUpdateHP;
        }

        private void Update()
        {
            playerPokemonHpImage.fillAmount = Mathf.Lerp(playerPokemonHpImage.fillAmount, targetPlayerHP, Time.deltaTime * speedTargetFill);
            enemyPokemonHpImage.fillAmount = Mathf.Lerp(enemyPokemonHpImage.fillAmount, targetEnemyHP, Time.deltaTime * speedTargetFill);

            CheckFillAmout(playerPokemonHpImage);
            CheckFillAmout(enemyPokemonHpImage);
        }

        void CheckFillAmout(Image i)
        {
            if (i.fillAmount <= 0.5f)
            {
                if (i.fillAmount <= 0.25f)
                {
                    i.color = Color.red;
                }
                else
                {
                    i.color = Color.orange;
                }
            }
            else
            {
                i.color = Color.green;
            }
        }

        private void OnUpdateHP(float playerHP, float enemyHP)
        {
            targetPlayerHP = playerHP;
            targetEnemyHP = enemyHP;
        }

        private void SetUI(PokemonSO playerPoke, string enemyPoke)
        {
            playerPokemonHpImage.fillAmount = 1;
            enemyPokemonHpImage.fillAmount = 1;
            
            playerPokemonNameText.text = playerPoke.Name;
            enemyPokemonNameText.text = enemyPoke;

            SetButtonAttack(0, playerPoke.Attack1);
            SetButtonAttack(1, playerPoke.Attack2);
            SetButtonAttack(2, playerPoke.Attack3);
            SetButtonAttack(3, playerPoke.Attack4);
        }

        private void SetButtonAttack(int index, AttackSO attack)
        {
            if (attack == null)
            {
                attacksButton[index].Image.color = Color.white;
                attacksButton[index].Text.text = "???";
            }
            else
            {
                            
                attacksButton[index].Image.color = PokemonUtils.GetColorFromType(attack.Type);
                attacksButton[index].Text.text = attack.Name;
            }
        }
        
        private void PannelChange(FightActions state)
        {
            Console.Print("PannelChange", ColorConsole.Red);
            
            for (int i = 0; i < pannelActions.Length; i++)
            {
                if (i == (int)state)
                {
                    pannelActions[i].SetActive(true);
                }
                else
                {
                    pannelActions[i].SetActive(false);
                }
            }
        }
    }
}