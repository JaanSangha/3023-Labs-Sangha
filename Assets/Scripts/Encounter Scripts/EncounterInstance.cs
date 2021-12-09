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

    public ICharacter CurrentCharacterTurn
    {
        get { return currentCharacterTurn; }
        set { currentCharacterTurn = value; }
    }

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
    private void Start()
    {
        currentCharacterTurn = player;
        currentCharacterTurn.TakeTurn(this);
        onPlayerTurnBegin.Invoke(player);
    }

    public void AdvanceTurns()
    {
        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        if(currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = enemy;
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
}
