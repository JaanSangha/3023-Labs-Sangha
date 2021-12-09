using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : ICharacter
{
    public float mana = 100;
    public float pHealth = 100;

    [SerializeField]
    private AICharacter opponent;
    [SerializeField]
    private EncounterInstance myEncounter;

    public override void TakeTurn(EncounterInstance encounter)
    {
        encounter = this.myEncounter;
        opponent = encounter.Enemy;
        Debug.Log("Player taking turn");
       // throw new System.NotImplementedException();
    }

    public void UseAbility(int slot)
    {
        abilities[slot].Cast(this, opponent);
        encounterUI.GetAbilityName(abilities[slot].Name);
        myEncounter.AdvanceTurns();
    }
}