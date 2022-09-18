using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltAction : MonoBehaviour
{

    public GameObject Cube;
    private bool isUpsideDown = false;
    private bool l = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Cross(Cube.transform.up,Cube.transform.right).y;
        if (angle < -0.5 && isUpsideDown == false) {
            isUpsideDown = true;
            

        }
        else if (angle > -0.5 && isUpsideDown == true)
        {
            isUpsideDown = false;
            l = !l;
        }
  
        Debug.Log("IsUpsideDown:"+  isUpsideDown.ToString());
        Debug.Log("Lights: "+l.ToString());
    }

}
