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

    public Slider characterManaSlider;
    public Slider characterHealthSlider;

    public float mana = 100;
    public float pHealth = 100;

    //first character is the caster, second is the target
    //public UnityEvent<Ability> onAbilityCast;
    private void Start()
    {
        characterManaSlider.value = mana;
        characterHealthSlider.value = pHealth;
    }
    //public void UseAbility(int abilitySlot, ICharacter self, ICharacter opponent)
    //{
    //    abilities[abilitySlot].Cast(self, opponent, encounter);
    //}

    public abstract void TakeTurn(EncounterInstance encounter);
}
