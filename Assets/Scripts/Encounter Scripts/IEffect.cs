using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEffect : ScriptableObject
{
    //[SerializeField]
    //int manaCost = 10;
    public abstract void ApplyEffect(ICharacter self, ICharacter other, EncounterInstance encounter);
}
