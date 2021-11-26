using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EncounterInstance : MonoBehaviour
{
    private int turnNumber;
    public int TurnNumber
    {
        get { return turnNumber; }
        private set { turnNumber = value; }
    }

    public PlayerCharacter player;
    public AiCharacter enemy;
    public ICharacter currentCharacter;

    public UnityEvent<PlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<AiCharacter> onEnemyTurnBegin;
    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = player;
        player.onAbilityCast.AddListener(OnAbilityCast);
    }

    public void OnAbilityCast(Ability casted)
    {
       // StartCoroutine(TurnTimeCoroutine());
        AdvanceTurns();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TurnTimeCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        AdvanceTurns();
    }

    public void TurnDelay()
    {
        StartCoroutine(TurnTimeCoroutine());
    }

    public void AdvanceTurns()
    {
        turnNumber++;
        if (currentCharacter == player)
        {
            currentCharacter = enemy;
            player.onAbilityCast.RemoveListener(OnAbilityCast);
            enemy.onAbilityCast.AddListener(OnAbilityCast);
           // StartCoroutine(TurnTimeCoroutine());
            onEnemyTurnBegin.Invoke(enemy);
        }
        else
        {
            currentCharacter = player;
            enemy.onAbilityCast.RemoveListener(OnAbilityCast);
            player.onAbilityCast.AddListener(OnAbilityCast);
            //StartCoroutine(TurnTimeCoroutine());
            onPlayerTurnBegin.Invoke(player);
        }

        currentCharacter.TakeTurn(this);

    }
}
