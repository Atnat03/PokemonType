using UnityEngine;

[CreateAssetMenu(fileName = "PokemonSO", menuName = "Attack")]
public class AttackSO : ScriptableObject
{
    public string Name;
    public int Damage;
    public PokemonType Type;
    public GameObject vfxPrefab;
    public float vfxDelay = 0.5f;
    public float postDelay = 1.5f;
    public Vector3 vfxOffset;
    public AnimationAttackType AnimationTrigger = AnimationAttackType.Throw;
}

public enum AnimationAttackType
{
    Throw, Charge
}