using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    PlaySound PScript;
    ManageLighting MLScript;
    GenerateAsyncBeat GABScript;
    private float speed;
    private Light myLight;
    private AudioSource myAudioSource;

    void Start()
    {
        MLScript = FindObjectOfType<ManageLighting>();
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
        switch (gameObject.tag)
        {
            case "Lblue":
                myLight = MLScript.L;
                myAudioSource = MLScript.LS;
                break;
            case "CLpurple":
                myLight = MLScript.CL;
                myAudioSource = MLScript.CLS;
                break;
            case "Cblue":
                myLight = MLScript.C;
                myAudioSource = MLScript.CS;
                break;
            case "CRgreen":
                myLight = MLScript.CR;
                myAudioSource = MLScript.CRS;
                break;
            case "Ryellow":
                myLight = MLScript.R;
                myAudioSource = MLScript.RS;
                break;
        }


        PScript = FindObjectOfType<PlaySound>();
        MLScript = FindObjectOfType<ManageLighting>();

        PScript.maxScore += 1;
        speed = FindObjectOfType<MainScript>().speed;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);

        if (transform.position.z < -17.3) {
            GABScript.missedTilesNumber += 1;
            MLScript.goRed(myLight);
            Destroy(gameObject);
        }
    }

    public void selfdestruct() {
        PScript.score += 1;
        GABScript.missedTilesNumber = 0;
        MLScript.goGreen(myLight);
        myAudioSource.Play();
        Destroy(gameObject);
    }
}

