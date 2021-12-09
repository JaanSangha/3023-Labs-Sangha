using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Non-Damage", menuName = "Abilities/Non Damage Effect")]
public class NonDamageEffect : IEffect
{
    [SerializeField]
    int StunAmount;
    public override void ApplyEffect(ICharacter self, ICharacter other)
    {
        Debug.Log("Stunned!");
    }
}
