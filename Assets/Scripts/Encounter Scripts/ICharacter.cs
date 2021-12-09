using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class ICharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] abilities;

    private EncounterInstance encounter;

    [SerializeField]
    protected EncounterUI encounterUI;

    public Slider characterMana;

    //first character is the caster, second is the target
    //public UnityEvent<Ability> onAbilityCast;
    private void Start()
    {
        characterMana.value = 100;
        characterMana.value = encounter.Player.mana;
        characterMana.value = encounter.Enemy.mana;
    }
    public void UseAbility(int abilitySlot, ICharacter self, ICharacter opponent)
    {
        abilities[abilitySlot].Cast(self, opponent);
    }

    public abstract void TakeTurn(EncounterInstance encounter);
}
