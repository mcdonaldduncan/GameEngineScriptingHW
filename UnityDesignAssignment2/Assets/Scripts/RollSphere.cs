using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSphere : MonoBehaviour
{

    public float InitialSpeed = .25f;
    public float yRotationAmount = .25f;
    public float yForceAmount = 5f;
    public ForceMode yForceMode = ForceMode.Impulse;
    private float currentSpeed;

    private Vector3 direction = Vector3.forward;
    private Rigidbody rb;
    private bool jumpRequested;
    private bool canRequestJump = true;

    
    

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = InitialSpeed;
        rb = GetComponent<Rigidbody>();
        
        
    }

    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            Debug.Log("Jump!");
            rb.AddForce(Vector3.up * yForceAmount, yForceMode);
            jumpRequested = false;
        }
        else
        {
            rb.AddForce(direction * currentSpeed * Time.fixedDeltaTime);
        }
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canRequestJump)
        {
            canRequestJump = false;
            jumpRequested = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collObj = collision.gameObject;
        if (collObj.CompareTag("Barrier"))
        {
            canRequestJump = true;
            

            // Reduce the speed by the sphere's angular drag
            currentSpeed -= rb.angularDrag;

            // Change direction
            direction = -direction;

            // Rotate the sphere on the y axis
            transform.Rotate(Vector3.up, yRotationAmount);
            Debug.LogFormat("current speed={0}, Current y angle:{1}", currentSpeed, transform.rotation.eulerAngles.y);
            

        }
        else if (collObj.CompareTag("Rail"))
        {
            canRequestJump = true;
        }
    }

    

}
