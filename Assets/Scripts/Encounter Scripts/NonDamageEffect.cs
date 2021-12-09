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
    int coolDownCount = 0;
    public override void ApplyEffect(ICharacter self, ICharacter other, EncounterInstance encounter)
    {
        if(coolDownCount <= 0)
        {
            coolDownCount = coolDown;
        }
        else if (coolDownCount > 0 && self.mana > manaCost)
        {
            self.mana -= manaCost;

            if(encounter.CurrentCharacterTurn == self)
            {
                encounter.CurrentCharacterTurn = other;
                coolDownCount--;
            }

            Debug.Log("Stunned!");
        }

        self.characterManaSlider.value = self.mana;
    }
}
