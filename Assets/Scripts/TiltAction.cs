using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TiltAction : MonoBehaviour
{

    public GameObject Cube;
    public GameObject Note;
    public int threshold;
    
    private float angle;
    private bool isLit = false;
    private bool isUD = false;
    private GameObject[] daytimeElements;
    private GameObject[] lightingElements;
    private string translatedText = "Apelles the painter. That is the way Leonardo da Vinci does it with all of his pictures," +
        " like, for example, with the countenance of Lisa del Giocondo and that of Anne, the mother of the Virgin. We will see " +
        "how he is going to do it regarding the great council chamber, the thing which he has just come to terms about with the " +
        "gonfaloniere. October 1503.";
    private string originalText = "Apelles pictor. Ita Leonardus Vincius facit in omnibus suis picturis, ut enim caput Lise del " +
        "Giocondo et Anne matris virginis. Videbimus, quid faciet de aula magni consilii, de qua re convenit iam cum vexillifero. " +
        "1503 octobris";
    
    // Start is called before the first frame update
    void Start()
    {
        lightingElements = GameObject.FindGameObjectsWithTag("C1_Lighting");
        daytimeElements = GameObject.FindGameObjectsWithTag("C1_Daytime");
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
            translatetext();
            isLit = !isLit;
            toggleLights();
        }

    }

    void toggleLights() {
        foreach (GameObject lightObject in lightingElements)
        {
            lightObject.SetActive(!lightObject.activeSelf);

        }
        foreach (GameObject daytimeObject in daytimeElements)
        {
            daytimeObject.SetActive(!daytimeObject.activeSelf);

        }
    }
    void translatetext()
    {   if (isLit == true){
            Note.GetComponent<TextMeshPro>().text = translatedText;
            Note.GetComponent<TextMeshPro>().fontSize = 13.5f;
        }
        else {
            Note.GetComponent<TextMeshPro>().text = originalText;
            Note.GetComponent<TextMeshPro>().fontSize = 15.6f;
        }
        
    }

}
