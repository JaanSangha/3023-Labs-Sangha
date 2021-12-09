using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Abilities/Damage Effect")]
public class DamageEffect : IEffect
{
    [SerializeField]
    int DamageAmount;
    [SerializeField]
    int manaCost = 10;
    public override void ApplyEffect(ICharacter self, ICharacter other, EncounterInstance encounter)
    {
        ApplySelfEffect(self);
        ApplyOpponentEffect(other);
        Debug.Log("Damage!");
    }

    void ApplySelfEffect(ICharacter self)
    {
        if (self.mana > manaCost)
        {
            self.mana -= manaCost;
        }
        self.characterManaSlider.value = self.mana;
    }

    void ApplyOpponentEffect(ICharacter other)
    {
        if(other.pHealth > 0)
        {
            other.pHealth -= DamageAmount;
        }
        other.characterHealthSlider.value = other.pHealth;
    }
}
