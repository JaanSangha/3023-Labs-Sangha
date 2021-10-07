using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject BattleScene;

    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    public bool canMove = true;

    public LayerMask randomEncounterLayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        BattleScene = GameObject.FindGameObjectWithTag("BattleScene");
        BattleScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //float inputX = Input.GetAxisRaw("Horizontal");
        //float inputY = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, inputY * moveSpeed * Time.deltaTime, 0);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        

        if (BattleScene.activeSelf)
        {
            canMove = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            canMove = true;
            Time.timeScale = 1.0f;
        }
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        }

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //{
            if (rigidbody.velocity.x != 0 || rigidbody.velocity.y != 0)
            {
                CheckForEncounter();
                Debug.Log("moving");
            }
       // }
    }

    void CheckForEncounter()
    {
        float p = Random.Range(1.0f, 1001.0f);

        if (Physics2D.OverlapCircle(transform.position, 0.01f, randomEncounterLayer) != null)
        {
            if (p <= 8)
            {
                //canMove = false;
                BattleScene.SetActive(true);
                Debug.Log("Encounter");
            }
        }

    }

}
