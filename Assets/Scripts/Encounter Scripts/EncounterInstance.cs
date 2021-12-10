using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EncounterInstance : MonoBehaviour
{

    public PlayerBehaviour playerInst;
    [SerializeField]
    private PlayerCharacter player;
    public GameObject PlayerRef;

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
        playerInst = FindObjectOfType<PlayerBehaviour>();
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

        if(player.pHealth <= 0 || enemy.pHealth <= 0)
        {
            GameOverState();
        }
    }

    public void EscapeEncounter()
    {
        playerInst.GetComponent<PlayerBehaviour>().SetCanMoveTrue();
        GameObject EncounterPrefab = GetComponentInParent<EncounterUI>().gameObject;
        Destroy(EncounterPrefab);
    }

    void GameOverState()
    {
        if (player.pHealth <= 0)
        {
            playerInst.GetComponent<PlayerBehaviour>().LoadLocation();
            playerInst.EncounterWinorLoss = false;
            //EscapeEncounter();
        }
        else if (enemy.pHealth <= 0)
        {
            playerInst.EncounterWinorLoss = true;
            //TakeEnemyAbilities();
            //EscapeEncounter();
        }
        Debug.Log("Character is dead");
        EscapeEncounter();
    }

    void TakeEnemyAbilities()
    {
        foreach(var ability in enemy.Abilities)
        {
            foreach(var playerAbility in player.Abilities)
            {
                if(ability.Name != playerAbility.Name)
                {
                    player.Abilities.Add(ability);
                }
            }
        }
    }
}
