using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float shotTimer = 0f;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer > 1)
        {
            Instantiate(enemyBullet, transform.position, transform.rotation);
            shotTimer = 0f;
        }
    }
}
