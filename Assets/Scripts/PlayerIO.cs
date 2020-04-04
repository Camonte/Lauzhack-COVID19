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

    public Animator anim;

    private PlayerMover mover;
    public PaintGun gun;
    private int colorChangeTimer = 0;

    public Vector3 delta = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    void Start()
    {
        mover = GetComponent<PlayerMover>();
        anim = GetComponent<Animator>();
        
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
        anim.SetFloat("Vertical", _velocity.magnitude);

        
        mover.Move(_velocity);
        
        float _jump = Input.GetAxisRaw("Jump");
        if(_jump!=0){
            anim.SetTrigger("Jump");
        }
        mover.Jump(_jump*transform.up*jumpForce);

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRotation*lookSensitivity, 0);
        mover.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0, 0);
        mover.RotateCamera(_cameraRotation*lookSensitivity);
        if (Input.GetButton("Fire1")){
            delta = new Vector3(Input.GetAxisRaw("Mouse Y"),0,Input.GetAxisRaw("Mouse X"))-lastPos;
            gun.Shoot(delta.normalized);
            
        }
        if(Input.GetButton("Fire2")){
            if (colorChangeTimer==0){
            gun.NextColor();
            colorChangeTimer=30;
            }
        }
        if (colorChangeTimer>0){
            colorChangeTimer--;
        }
        

        lastPos = new Vector3(Input.GetAxisRaw("Mouse Y"),0,Input.GetAxisRaw("Mouse X"));



    }




}
