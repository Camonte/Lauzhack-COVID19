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

    public GameObject Brush;
    public Transform bulletSpawn;
    public PlayerMover player;
    // Start is called before the first frame update

    // Update is called once per frame

    public void Shoot()
    {
      RaycastHit hit;
      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, range )){  
          
          if(hit.rigidbody!= null){
          hit.rigidbody.AddForce(bulletSpawn.forward*damage);

          }
          //Quaternion.AngleAxis(-90, Vector3.right)*
          Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
          var go = Instantiate(Brush, hit.point+0.1f*hit.normal, rot, hit.transform);
          Debug.Log(hit.normal);
          go.transform.localScale = Vector3.one*0.01f;
          player.ApplyForce(-1*bulletSpawn.forward*damage*recoil/10);
      }
    }
}
