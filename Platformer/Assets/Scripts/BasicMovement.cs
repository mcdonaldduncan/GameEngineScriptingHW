using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public int speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        Vector3 rot = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);

        if (transform.position.x <= -3.0f)
        {
            transform.rotation = Quaternion.Euler(rot);
        }
        if (transform.position.x >= 3.0f)
        {
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
