using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TiltAction2 : MonoBehaviour
{

    public GameObject Cube;
    public int threshold;
    
    private float angle;
    private bool isLit = false;
    private bool isUD = false;
    private GameObject[] daytimeElements;
    private GameObject[] lightingElements;

    // Start is called before the first frame update
    void Start()
    {
        lightingElements = GameObject.FindGameObjectsWithTag("C2_Lighting");
        daytimeElements = GameObject.FindGameObjectsWithTag("C2_Daytime");
        foreach (GameObject lightObject in lightingElements)
        {
            lightObject.SetActive(false);

        }
        foreach (GameObject daytimeObject in daytimeElements)
        {
            daytimeObject.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        angle = Cube.transform.localEulerAngles.x;
        if (angle > 0 && angle < threshold && isUD == false)
        {
            
            isUD = true;
        }
        else if (angle > threshold && isUD == true) {
            isUD = false;
            isLit = !isLit;
            toggleLights();
        }

    }

    void toggleLights() {
        foreach(GameObject lightObject in lightingElements)
        {
            lightObject.SetActive(!lightObject.activeSelf);
            
        }
        foreach (GameObject daytimeObject in daytimeElements)
        {
            daytimeObject.SetActive(!daytimeObject.activeSelf);

        }
    }
    
}
