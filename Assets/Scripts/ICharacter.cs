using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] abilities;

    private EncounterInstance encounter;
    //first character is the caster, second is the target
    public UnityEvent<Ability> onAbilityCast;

    public void UseAbility(int abilitySlot, ICharacter self, ICharacter opponent)
    {
        abilities[abilitySlot].Cast(self, opponent);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void TakeTurn(EncounterInstance encounter);
}
