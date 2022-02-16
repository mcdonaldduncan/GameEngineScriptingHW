using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClick : MonoBehaviour
{
    /// <summary>
    /// Reference to the script that is used by the parent object, Table.
    /// </summary>
    private Dealer dealer;
    
    /// <summary>
    /// Get a reference to the Table and then 
    /// to it's script. OnMouseDown calls 
    /// a method on dealer.
    /// </summary>
    void Start()
    {
        // Ger 
        GameObject table =
        this.transform.parent.gameObject;

        dealer = table.GetComponent<Dealer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Hello from Mouse Down");
        if (dealer != null)
            dealer.DisplayCardInfo(this.gameObject);
    }
}
