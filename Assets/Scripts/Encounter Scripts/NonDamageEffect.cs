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
    [SerializeField]
    int coolDownCounter = 1;

    public override void ApplyEffect(ICharacter self, ICharacter other, EncounterInstance encounter)
    {
        if (this.name == "Stun")
        {
            ApplyStunEffect(self, other, encounter);
        }

        if(this.name == "RestEffect" && self.mana < 100)
        {
            self.mana += manaCost;
        }

        if (this.name == "EscapeEffect")
        {
            if(encounter.CurrentCharacterTurn.tag == "Player")
            {
                Debug.Log("Escape Route");
                encounter.playerInst.SoundMngr.GetComponent<SoundManager>().FadeOutEncounter();
                encounter.playerInst.SetCanMoveTrue();
                encounter.EscapeEncounter();
            }
        }

        Debug.Log(this.name);
        self.characterManaSlider.value = self.mana;
    }

    void ApplyStunEffect(ICharacter self, ICharacter other, EncounterInstance encounter)
    {
        if (coolDownCounter > 0 && self.mana > manaCost)
        {
            self.mana -= manaCost;

            if (encounter.CurrentCharacterTurn == self)
            {
                // Passes the current turn as the opponent to skip
                // the opponent's turn
                encounter.CurrentCharacterTurn = other;
            }
            Debug.Log("Stunned!");
            coolDownCounter--;
        }
        else if (coolDownCounter <= 0)
        {
            coolDownCounter = coolDown;
        }
    }
}
