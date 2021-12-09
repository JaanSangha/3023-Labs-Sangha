using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonManager : MonoBehaviour
{

    [SerializeField]
    private EncounterInstance encounter;

    [SerializeField]
    private GameObject abilitiesPanel;

    [SerializeField]
    private float textPromptSecondsPerCharacter = 0.1f;

    GameObject soundManager;
    public GameObject PlayerRef;
    public GameObject BattleScene;
    public Slider PlayerMana;
    public Slider EnemyMana;
    public Text feedbacktext;
    public bool playerTurn;

    private IEnumerator animateTextCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        PlayerRef.GetComponent<PlayerBehaviour>().SetCanMoveFalse();
        animateTextCoroutine = AnimateTextCoroutine("You have encountered a wild enemy! Choose an Ability.", textPromptSecondsPerCharacter);
        StartCoroutine(animateTextCoroutine);

        //encounter.Player.onAbilityCast.AddListener(AnnouncePlayerMoveUsed);
        //encounter.Enemy.onAbilityCast.AddListener(AnnounceEnemyMoveUsed);
        //playerTurn = true;
        //encounter.Player.mana = 100;
        //encounter.Enemy.mana = 100;
        //PlayerMana.value = encounter.Player.mana;
        //EnemyMana.value = encounter.Enemy.mana;
    }
    //void AnnouncePlayerMoveUsed(Ability name)
    //{
    //    if(animateTextCoroutine != null)
    //    {
    //        StopCoroutine(animateTextCoroutine);
    //    }
    //    animateTextCoroutine = AnimateTextCoroutine("Player Used " + name, textPromptSecondsPerCharacter);
    //    //PlayerMana.value = encounter.Player.mana;
    //    StartCoroutine(animateTextCoroutine);
    //}

    //void AnnounceEnemyMoveUsed(Ability name)
    //{
    //    if (animateTextCoroutine != null)
    //    {
    //        StopCoroutine(animateTextCoroutine);
    //    }
    //    animateTextCoroutine = AnimateTextCoroutine("Enemy Used " + name, textPromptSecondsPerCharacter);
    //    EnemyMana.value = encounter.Enemy.mana;
    //    StartCoroutine(animateTextCoroutine);
    //}

    IEnumerator AnimateTextCoroutine(string message, float secondsPerCharacter = 0.1f)
    {
        abilitiesPanel.SetActive(false);
        feedbacktext.text = ("");

        for (int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            feedbacktext.text += message[currentChar];
            yield return new WaitForSeconds(secondsPerCharacter);
        }

        abilitiesPanel.SetActive(true);
        animateTextCoroutine = null;
    }

    IEnumerator enemyTurnCoroutine()
    {
        feedbacktext.text = ("Enemies turn...");

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        feedbacktext.text = ("Player's turn...");
        playerTurn = true;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);   
    }

    public void OnSkipButtonPressed()
    {
        if (playerTurn)
        {
            playerTurn = false;
            StartCoroutine(enemyTurnCoroutine());
        }
    }
    public void OnRunButtonPressed()
    {
        if (playerTurn)
        {
            soundManager.GetComponent<SoundManager>().FadeOutEncounter();
            PlayerRef.GetComponent<PlayerBehaviour>().SetCanMoveTrue();
            Destroy(this.gameObject);
        }
    }
}
