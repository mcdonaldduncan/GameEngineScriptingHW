using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject bullet;

    float timeUntilNextShot;
    float timeBetweenShots = 1.0f;

    bool canShoot = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            canShoot = false;
            timeUntilNextShot = Time.time + timeBetweenShots;
        }

        if (Time.time > timeUntilNextShot)
        {
            canShoot = true;
        }
        
    }
}
