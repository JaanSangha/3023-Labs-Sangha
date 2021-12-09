using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Non-Damage", menuName = "Abilities/Non Damage Effect")]
public class NonDamageEffect : IEffect
{
    [SerializeField]
    int StunAmount;
    [SerializeField]
    int manaCost = 30;
    [SerializeField]
    int coolDown = 1; // Number of turns the non damage has effect each time
    public override void ApplyEffect(ICharacter self, ICharacter other, EncounterInstance encounter)
    {
        if (coolDown > 0 && self.mana > manaCost)
        {
            self.mana -= manaCost;

        }
        Debug.Log("Stunned!");
    }
}
