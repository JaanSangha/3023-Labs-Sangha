using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Abilities/Effect")]
public class DamageEffect : IEffect
{
    [SerializeField]
    int DamageAmount;

    public override void ApplyEffect(ICharacter self, ICharacter other)
    {
        Debug.Log("Damage!");
    }
}
