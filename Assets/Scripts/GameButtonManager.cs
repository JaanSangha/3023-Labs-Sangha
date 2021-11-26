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
        animateTextCoroutine = AnimateTextCoroutine("You have encountered a wild enemy! Choose an Ability.", textPromptSecondsPerCharacter);
        StartCoroutine(animateTextCoroutine);

        encounter.player.onAbilityCast.AddListener(AnnouncePlayerMoveUsed);
        encounter.enemy.onAbilityCast.AddListener(AnnounceEnemyMoveUsed);
        //playerTurn = true;
        encounter.player.mana = 100;
        encounter.enemy.mana = 100;
        PlayerMana.value = encounter.player.mana;
        EnemyMana.value = encounter.enemy.mana;
    }
    void AnnouncePlayerMoveUsed(Ability name)
    {
        if(animateTextCoroutine != null)
        {
            StopCoroutine(animateTextCoroutine);
        }
        animateTextCoroutine = AnimateTextCoroutine("Player Used " + name, textPromptSecondsPerCharacter);
        PlayerMana.value = encounter.player.mana;
        StartCoroutine(animateTextCoroutine);
    }

    void AnnounceEnemyMoveUsed(Ability name)
    {
        if (animateTextCoroutine != null)
        {
            StopCoroutine(animateTextCoroutine);
        }
        animateTextCoroutine = AnimateTextCoroutine("Enemy Used " + name, textPromptSecondsPerCharacter);
        EnemyMana.value = encounter.enemy.mana;
        StartCoroutine(animateTextCoroutine);
    }

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
            PlayerRef.GetComponent<PlayerBehaviour>().SetCanMoveTrue();
            Destroy(this.gameObject);
        }
    }
}
