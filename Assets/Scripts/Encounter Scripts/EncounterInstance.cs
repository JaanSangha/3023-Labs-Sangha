using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter player;

    public PlayerCharacter Player
    {
        get { return player; }
        private set { player = value; }
    }

    [SerializeField]
    private AICharacter enemy;

    public AICharacter Enemy
    {
        get { return enemy; }
        private set { enemy = value; }
    }

    [SerializeField]
    private ICharacter currentCharacterTurn;
    //Events
    public UnityEvent<ICharacter> onCharacterTurnBegin;
    public UnityEvent<ICharacter> onCharacterTurnEnd;

    public UnityEvent<PlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<PlayerCharacter> onPlayerTurnEnd;

    public UnityEvent<AICharacter> onEnemyTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnEnd;

    //Turn counter
    private int turnNumber;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacterTurn = player;
        onPlayerTurnBegin.Invoke(player);
    }

    public void AdvanceTurns()
    {
        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        if (currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = Enemy;
        }
        else
        {
            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
        }
        turnNumber++;

        onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);
    }

    /*
    private int turnNumber;
    public int TurnNumber
    {
        get { return turnNumber; }
        private set { turnNumber = value; }
    }

    public int eHealth = 100;
    public PlayerCharacter player;
    public AICharacter enemy;
    public ICharacter currentCharacter;

    public UnityEvent<PlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnBegin;
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
    */
}
