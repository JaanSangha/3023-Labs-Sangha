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

    protected TMPro.TextMeshProUGUI HealthBarText;
    protected TMPro.TextMeshProUGUI ManaBarText;

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

    private void Update()
    {
        HealthBarText.text = pHealth.ToString() + "%";
        ManaBarText.text = mana.ToString() + "%";
    }

    public abstract void TakeTurn(EncounterInstance encounter);
}