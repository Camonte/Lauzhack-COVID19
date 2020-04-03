using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private bool grounded = false;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 force = Vector3.zero;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    public void Move(Vector3 _velocity){
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation){
        rotation = _rotation;
    }
    public void Jump(Vector3 _jump){
        if (grounded){
        force+=_jump;
        }
    }
    public void ApplyForce(Vector3 _force){
        force+=_force;
        
    }

    public void RotateCamera(Vector3 _cameraRotation){
        cameraRotation = _cameraRotation;
    }


    void FixedUpdate(){
        ApplyForce();
        PerformMovement();
        PerformRotation();
        PerformCameraRotation();
       
        
    }
    void PerformMovement(){
        if (velocity!=Vector3.zero){
            rigidBody.MovePosition(rigidBody.position+velocity*Time.fixedDeltaTime);
        }
    }
    void PerformRotation(){
        if (rotation!=Vector3.zero){
            rigidBody.MoveRotation(rigidBody.rotation*Quaternion.Euler(rotation));
        }
    }
    void PerformCameraRotation(){
        if (cam!=null){
            cam.transform.Rotate(-cameraRotation);
        }
    }



    void ApplyForce(){
        if (force!=Vector3.zero){
            rigidBody.AddForce(force);
            force = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.tag == "Ground"){
          grounded = true;
      }
    }
    void OnCollisionExit(Collision collisionInfo){
         if(collisionInfo.collider.tag == "Ground"){
          grounded = false;
      }
    }


}
