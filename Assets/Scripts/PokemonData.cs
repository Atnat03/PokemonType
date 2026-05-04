using System;
using UnityEngine;

namespace CommandPattern
{
    [CreateAssetMenu(fileName = "PokemonData", menuName = "PokemonData")]
    public class PokemonSO : ScriptableObject
    {
        public PokemonBehavior prefab;
        public string Name;
        public PokemonType type;
        public int HP;
        public Sprite icon;
        public PokemonAttack Attack1;
        public PokemonAttack Attack2;
    }

    [Serializable]
    public class PokemonAttack
    {
        public string Name;
        public int Damage;
        public PokemonType Type;
    }

    public enum PokemonType
    {
        Fire, Water, Grass, Electric, Fighting, Psychic, Normal
    }
}