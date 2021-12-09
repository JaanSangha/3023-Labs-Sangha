using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Camera playerCamera;
   // public GameObject BattleScene;
    public GameObject BattleScenePrefab;

    GameObject soundManager;

    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    public bool canMove = true;

    public float yDirection;

    private Animator playerAnimator;

    public LayerMask randomEncounterLayer;


    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        rigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        transform.position = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), 0);
        canMove = true;
        GameSaver.OnSaveEvent.AddListener(SaveLocation);
        GameSaver.OnLoadEvent.AddListener(LoadLocation);
    }

    // Update is called once per frame
    void Update()
    {
        //float inputX = Input.GetAxisRaw("Horizontal");
        //float inputY = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, inputY * moveSpeed * Time.deltaTime, 0);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        

        //if (BattleScene.activeSelf)
        //{
        //    canMove = false;
        //    Time.timeScale = 0.0f;
        //}
        //else
        //{
        //    canMove = true;
        //    Time.timeScale = 1.0f;
        //}

        playerAnimator.SetFloat("yVelocity", rigidbody.velocity.y);
        playerAnimator.SetFloat("xVelocity", rigidbody.velocity.x);

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayRandomEncounterDebug();
        }

    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        // {
            if (canMove)
            {
                rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
            }
        // }

        if (rigidbody.velocity.x != 0 || rigidbody.velocity.y != 0)
        {
            CheckForEncounter();
            Debug.Log("moving");
        }

    }

    IEnumerator CameraShake(float ShakeTime)
    {
        Vector3 CamPos = playerCamera.transform.position;
        float shakeMagnitude = 0.1f;

        while (ShakeTime > 0)
        {
            playerCamera.transform.position = CamPos + Random.insideUnitSphere * shakeMagnitude;

            ShakeTime -= Time.deltaTime * 0.8f;
            yield return null;
        }
       
        ShakeTime = 0f;
        transform.localPosition = CamPos;

        PlayRandomEncounterDebug();
    }

    public void SetCanMoveTrue()
    {
        canMove = true;
    }

    public void SetCanMoveFalse()
    {
        canMove = false;
    }

    void CheckForEncounter()
    {
        float p = Random.Range(1.0f, 1001.0f);

        if (Physics2D.OverlapCircle(transform.position, 0.01f, randomEncounterLayer) != null)
        {
            if (p <= 8)
            {
                canMove = false;
                StartCoroutine(CameraShake(2.0f));
               // PlayRandomEncounterDebug();
            }
        }

    }

    void PlayRandomEncounterDebug()
    {
        //canMove = false;
        //BattleScene.SetActive(true);
        Instantiate(BattleScenePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        soundManager.GetComponent<SoundManager>().PlayEncounterMusic();
        Debug.Log("Encounter");
    }

    void SaveLocation()
    {
        PlayerPrefs.SetString("Location", "Loacation X: " + transform.position.x + " Location Y: " + transform.position.y);
        PlayerPrefs.SetFloat("XPosition", transform.position.x);
        PlayerPrefs.SetFloat("YPosition", transform.position.y);
        Debug.Log("LocationSaved");
    }
    void LoadLocation()
    {
        string loadLocation = PlayerPrefs.GetString("Location", "");
        transform.position = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), 0);
        Debug.Log(loadLocation);
        Debug.Log("LocationLoaded");

    }
}
