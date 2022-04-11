using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField] GameBehavior GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(transform.gameObject);
            Debug.Log("Item Collected!");

            GameManager.Items += 1;
        }
    }
}
