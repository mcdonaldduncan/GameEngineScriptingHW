using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMonster : MonoBehaviour
{
    public string monKey;
    public Monster monster;

    public float speed;
    public int max;

    // Start is called before the first frame update
    void Start()
    {
        monster = MonsterFactory.Instance.GetMon(monKey);
        monster.monMonvement = new MonsterMovement();
        monster.monMonvement.position = this.transform.position;
        monster.monMonvement.Speed = speed;
        monster.monMonvement.max = max;
    }

    private void Update()
    {
        //monster.monMonvement.DetermineState();
        //this.transform.position = monster.monMonvement.position;
    }
}
