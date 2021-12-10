using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGamePressed()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        titleScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void CreditsButton()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
}
