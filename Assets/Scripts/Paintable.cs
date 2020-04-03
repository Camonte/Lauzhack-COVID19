using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
     public GameObject brush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void paintAt(Transform transform){
        var go = Instantiate(brush, transform.position,transform.rotation );
    }
}
