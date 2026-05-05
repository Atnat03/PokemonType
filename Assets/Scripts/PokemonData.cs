using System;
using Pokemons;
using UnityEngine;

[CreateAssetMenu(fileName = "PokemonSO", menuName = "PokemonSO")]
public class PokemonSO : ScriptableObject
{
    public PokemonBehavior prefab;
    public string Name;
    public PokemonType type;
    public int HP;
    public Sprite icon;
    public AttackSO Attack1;
    public AttackSO Attack2;
    public AttackSO Attack3;
    public AttackSO Attack4;
}

public enum PokemonType
{
    Fire, Water, Grass, Electric, Fighting, Psychic, Normal
}

public static class PokemonUtils
{
    public static Color GetColorFromType(PokemonType type)
    {
        switch (type)
        {
            case PokemonType.Fire: return Color.red;
            case PokemonType.Water: return Color.dodgerBlue;
            case PokemonType.Grass: return Color.green;
            case PokemonType.Electric: return Color.yellow;
            case PokemonType.Fighting: return Color.saddleBrown;
            case PokemonType.Psychic: return Color.purple;
            case PokemonType.Normal: return Color.white;
            default: return Color.white;
        }
    }
    
    static readonly float[,] chart =
    {
        {0.5f,0.5f,2f,  1f,  1f,  1f,  1f},//Fire
        {2f,  0.5f,0.5f,1f,  1f,  1f,  1f},//Water
        {0.5f,2f,  0.5f,1f,  1f,  1f,  1f},//Grass
        {1f,  2f,  0.5f,0.5f,1f,  1f,  1f},//Elec
        {1f,  1f,  1f,  1f,  1f,  0.5f,2f},//Figh
        {1f,  1f,  1f,  1f,  2f,  0.5f,1f},//Psy 
        {1f,  1f,  1f,  1f,  1f,  1f,  1f} //Norm 
    };
    
    public static float DamageTypeMultiplier(PokemonType attackerType, PokemonType receverType)
    {
        return chart[(int)attackerType, (int)receverType];
    }
}