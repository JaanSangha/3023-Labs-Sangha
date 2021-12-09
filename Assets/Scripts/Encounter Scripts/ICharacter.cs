using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] abilities;

    private EncounterInstance encounter;

    [SerializeField]
    protected EncounterUI encounterUI;
    
    //first character is the caster, second is the target
    //public UnityEvent<Ability> onAbilityCast;

    public void UseAbility(int abilitySlot, ICharacter self, ICharacter opponent)
    {
        abilities[abilitySlot].Cast(self, opponent);
    }

    public abstract void TakeTurn(EncounterInstance encounter);
}
