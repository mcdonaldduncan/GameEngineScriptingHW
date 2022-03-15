using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);

    Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = _target.TransformPoint(CamOffset);

        transform.LookAt(_target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
