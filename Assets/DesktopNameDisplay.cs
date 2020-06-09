using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

namespace DesktopProject{
public class DesktopNameDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string value)
    {
        var txt = this.GetComponent<Text>();
        txt.text = value;
    }
}
}
