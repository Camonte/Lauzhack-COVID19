using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintGun : MonoBehaviour
{

    [SerializeField]
    private float velocity = 100f;
    [SerializeField]
    private float damage = 100000f;
    [SerializeField]
    private float range = 1000f;
    [SerializeField]
    private float recoil = 10f;
    [SerializeField]
    private float BrushSize = 0.01f;

    private int count = 0;
    
    public GameObject BrushRed;
    public GameObject BrushBlue;
    public GameObject BrushGreen;
    private GameObject Brush;
    public Transform bulletSpawn;
    public PlayerMover player;
    // Start is called before the first frame update

    // Update is called once per frame

    public void Shoot()
    {
      if(Brush==null){
        NextColor();
      }
      RaycastHit hit;
      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, range )){  
          
          if(hit.rigidbody!= null){
          hit.rigidbody.AddForce(bulletSpawn.forward*damage);
          }
          if(hit.transform.gameObject.CompareTag("Paintable"))
          { 
            Debug.Log(hit.transform.name);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var go = Instantiate(Brush, hit.point+0.01f*hit.normal, rot, hit.transform);
            go.transform.localScale = Vector3.one*BrushSize;
          }
          
          //Quaternion.AngleAxis(-90, Vector3.right)*
          player.ApplyForce(-1*bulletSpawn.forward*damage*recoil/10);
      }
    }
    public void NextColor(){
      if (count==0){
        Brush = BrushBlue;
        
      }
      else if(count == 1){
        Brush = BrushGreen;
      }
      else {
        Brush = BrushRed;
      }
      count =(count+1)%3;
    }
    

}
