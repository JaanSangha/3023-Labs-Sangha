using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private EncounterInstance encounter;

    [SerializeField]
    private GameObject abilitiesPanel;

    [SerializeField]
    private TMPro.TextMeshProUGUI encounterTextBox;

    [SerializeField]
    private float textPromptSecondsPerCharacter = 0.1f;

    private IEnumerator animateTextCoroutine = null; // If coroutine is running, this will not be null.

    private string currentEncounterName = "";
    // Start is called before the first frame update
    void Start()
    {
        //Disable abilities panel
        //Say something
        //Enable abilities panel
        animateTextCoroutine = AnimateTextCoroutine("You have encountered a: " + "Foo", textPromptSecondsPerCharacter);
        StartCoroutine(animateTextCoroutine);
        //StopCoroutine(animateTextCoroutine);

        //OnCharacterTurnBegin, announce whose turn it is
        //On Player turn begin, enable UI
        encounter.onCharacterTurnBegin.AddListener(AnnounceCharacterTurnBegin);
        //encounter.onCharacterTurnEnd.AddListener(AnnounceCharacterTurnEnd);
        //On Player turn begin, enable UI
        encounter.onPlayerTurnBegin.AddListener(EnablePlayerUI);
        //On Player turn end, disable UI
        encounter.onPlayerTurnEnd.AddListener(DisablePlayerUI);
    }

    void AnnounceCharacterTurnBegin(ICharacter characterTurn)
    {
        if (animateTextCoroutine != null)
        {
            StopCoroutine(animateTextCoroutine);
        }
        animateTextCoroutine = AnimateTextCoroutine("It is " + characterTurn.name + "'s turn", textPromptSecondsPerCharacter);
        //Debug.Log("Animated Begin Text");
        StartCoroutine(animateTextCoroutine);
    }

    void AnnounceCharacterTurnEnd(ICharacter characterTurn)
    {
        if (animateTextCoroutine != null)
        {
            StopCoroutine(animateTextCoroutine);
        }
        animateTextCoroutine = AnimateTextCoroutine("Hello World", textPromptSecondsPerCharacter);

        //animateTextCoroutine = AnimateTextCoroutine(characterTurn.name + " has used Ability: " + 
        //    currentEncounterName, textPromptSecondsPerCharacter);
        Debug.Log("Animated End Text");
        StartCoroutine(animateTextCoroutine);
    }

    public void GetAbilityName(string name)
    {
        currentEncounterName = name;
    }

    void EnablePlayerUI(ICharacter characterTurn)
    {
        abilitiesPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        abilitiesPanel.SetActive(false);
    }

    //Coroutine to write our text intro/prompt
    //It will type characters out one-by-one over time, because it looks nice
    //e.g. "Hello world"
    //H
    //He
    //Hel
    //Hell
    //Hello
    IEnumerator AnimateTextCoroutine(string message, float secondsPerCharacter = 0.1f)
    {
        //abilitiesPanel.SetActive((false));
        //Set text to blank
        encounterTextBox.text = "";

        //then over time, add letters until complete
        for (int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            encounterTextBox.text += message[currentChar];
            yield return new WaitForSeconds(secondsPerCharacter);
        }
        //Debug.Log("Animated Text");
        //abilitiesPanel.SetActive((true));
        animateTextCoroutine = null;
    }
}
