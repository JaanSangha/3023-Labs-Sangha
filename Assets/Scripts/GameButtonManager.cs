using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonManager : MonoBehaviour
{
    public GameObject BattleScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void OnRunButtonPressed()
    {
        BattleScene.SetActive(false);
       
    }
}
