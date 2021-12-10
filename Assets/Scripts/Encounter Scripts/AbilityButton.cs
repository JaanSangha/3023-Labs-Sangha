using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    private PlayerCharacter player;
    private TMPro.TextMeshProUGUI ButtonTxt;
    private Button button;

    public int abilitySlot = 0;

    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        ButtonTxt = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { player.UseAbility(abilitySlot); });

        ButtonTxt.text = player.Abilities[abilitySlot].Name;
    }
}
