using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float RotateSpeed = 75f;
    [SerializeField] float JumpVelocity = 5f;
    [SerializeField] float DistanceToGround = 0.1f;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed = 100f;

    CapsuleCollider _col;

    bool _isShooting;
    bool _isJumping;

    float _vInput;
    float _hInput;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
        _isShooting |= Input.GetMouseButtonDown(0);

        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;



        //transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        
        //transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }

        _isShooting = false;

        if (_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }

        _isJumping = false;

        Vector3 rotation = Vector3.up * _hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.deltaTime);

        _rb.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
