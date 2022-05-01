using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    public PlayerInventory playerInventory;

    public static Player Instance;

    public int movementIncrement;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = new PlayerMovement(transform.position, movementIncrement);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        playerInventory = new PlayerInventory();
        canMove = true;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        float xInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        float yInput = Input.GetAxis("Vertical") * Time.deltaTime;
        //Debug.Log(xInput);

        //rb.AddForce(Vector3.right * xInput, ForceMode2D.Impulse);
        //rb.AddForce(Vector3.up * yInput, ForceMode2D.Impulse);


        if (xInput > 0)
        {
            //Debug.Log("move right");
            //playerMovement.MoveRight(xInput);
            transform.Translate(xInput * Vector3.right * movementIncrement);
        }

        if (xInput < 0)
        {
            //playerMovement.MoveLeft(xInput);
            transform.Translate(xInput * Vector3.right * movementIncrement);
        }

        if (yInput > 0)
        {
            //playerMovement.MoveUp(yInput);
            transform.Translate(yInput * Vector3.up * movementIncrement);
        }

        if (yInput < 0)
        {
            //playerMovement.MoveDown(yInput);
            transform.Translate(yInput * Vector3.up * movementIncrement);
        }

        //Debug.Log(playerMovement.position);
        //this.transform.position = playerMovement.position;
    }

    
}
