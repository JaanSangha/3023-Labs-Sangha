using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, inputY * moveSpeed * Time.deltaTime, 0);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10); 
        //rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
    }

    //void FixedUpdate()
    //{
    //    float inputX = Input.GetAxisRaw("Horizontal");
    //    float inputY = Input.GetAxisRaw("Vertical");

    //    rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
    //}
}
