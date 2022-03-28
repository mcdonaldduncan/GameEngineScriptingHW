using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()

    {

        spriteRenderer = this.GetComponent<SpriteRenderer>();

        Debug.Log($"SpriteRenderer name is {spriteRenderer.name}"); // A - Will this line cause an error?

        Debug.Log("Start method working normally");

    }
}
