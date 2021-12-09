using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewAbility", menuName ="AbilitySystem/Ability")]
public class Ability : ScriptableObject
{

    [SerializeField]
    private new string name;

    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    [SerializeField]
    private string description;

    [SerializeField]
    private IEffect[] effects;


    public void Cast(ICharacter self, ICharacter other)
    {
        Debug.Log("Used: " + name);
        foreach (IEffect effect in effects)
        {
            effect.ApplyEffect(self, other);
        }

    }
}
