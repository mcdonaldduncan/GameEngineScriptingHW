using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float RotateSpeed = 75f;

    float _vInput;
    float _hInput;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        //transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        
        //transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.deltaTime);

        _rb.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
