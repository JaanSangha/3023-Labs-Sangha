using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : ICharacter
{
    public float mana = 100;

    public EncounterInstance myEncounter;
    public override void TakeTurn(EncounterInstance encounter)
    {
       // throw new System.NotImplementedException();
    }

    public void CastAbility(int slot)
    {
        if (mana > 24)
        {
            mana -= 25;
           // myEncounter.TurnDelay();
            Debug.Log("-25 Mana");
            UseAbility(slot, this, myEncounter.enemy);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
