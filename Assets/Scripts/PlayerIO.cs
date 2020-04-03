using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerIO : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 100f;
    [SerializeField]
    private float lookSensitivity = 5f;
    [SerializeField]
    // Start is called before the first frame update

    private PlayerMover mover;
    public PaintGun gun;
    
    void Start()
    {
        mover = GetComponent<PlayerMover>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 _movHorizontal = transform.right*_xMov;
        Vector3 _movVertical = transform.forward*_zMov;
        Vector3 _velocity = (_movHorizontal+_movVertical).normalized * speed;
        mover.Move(_velocity);
        
        float _jump = Input.GetAxisRaw("Jump");
        mover.Jump(_jump*transform.up*jumpForce);

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRotation*lookSensitivity, 0);
        mover.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0, 0);
        mover.RotateCamera(_cameraRotation*lookSensitivity);
        if (Input.GetButton("Fire1")){
            gun.Shoot();
        }

    }




}
