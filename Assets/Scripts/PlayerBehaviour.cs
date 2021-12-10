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

    public AudioSource audiosource;
    public AudioClip normalStep;
    public AudioClip grassStep;

    public GameObject SoundMngr
    {
        get { return soundManager; }
        private set { soundManager = value; }
    }

    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    public bool canMove = true;

    public float yDirection;

    private Animator playerAnimator;

    public LayerMask randomEncounterLayer;

    public bool EncounterWinorLoss = false;

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

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
                audiosource.Stop();
        }

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

    //coroutine to shake camera when encounter is found
    IEnumerator CameraShake(float ShakeTime)
    {
        float CamPos = playerCamera.transform.position.x;
        float shakeMagnitude = 0.1f;

        while (ShakeTime > 0)
        { 
            playerCamera.transform.position = new Vector3((CamPos + Random.Range(0,2) * shakeMagnitude), playerCamera.transform.position.y, playerCamera.transform.position.z);
            ShakeTime -= Time.deltaTime * 0.8f;
            yield return null;
        }
       
        ShakeTime = 0f;
        playerCamera.transform.position = new Vector3(CamPos, playerCamera.transform.position.y, playerCamera.transform.position.z);

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

    //randomly start encounter
    void CheckForEncounter()
    {
        float p = Random.Range(1.0f, 1001.0f);

        if (Physics2D.OverlapCircle(transform.position, 0.01f, randomEncounterLayer) != null)
        {
            if (p <= 9)
            {
                audiosource.Stop();
                canMove = false;
                rigidbody.velocity = new Vector3(0,0,0);
                StartCoroutine(CameraShake(1.50f));
            }
        }

    }

    void PlayRandomEncounterDebug()
    {
        Instantiate(BattleScenePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        soundManager.GetComponent<SoundManager>().PlayEncounterMusic();
        Debug.Log("Encounter");
    }

    //save player location
    void SaveLocation()
    {
        PlayerPrefs.SetString("Location", "Loacation X: " + transform.position.x + " Location Y: " + transform.position.y);
        PlayerPrefs.SetFloat("XPosition", transform.position.x);
        PlayerPrefs.SetFloat("YPosition", transform.position.y);
        Debug.Log("LocationSaved");
    }
    //load player location
    void LoadLocation()
    {
        string loadLocation = PlayerPrefs.GetString("Location", "");
        transform.position = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), 0);
        Debug.Log(loadLocation);
        Debug.Log("LocationLoaded");

    }
}
