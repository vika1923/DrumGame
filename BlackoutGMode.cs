using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutGMode : MonoBehaviour
{
    GenerateAsyncBeat GABScript;
    MainScript MScript;

    private void Start()
    {
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
        MScript = FindObjectOfType<MainScript>();
    }

    public void startBlackout()
    {
        if (MScript.extraTiles)
        {
            GABScript.addFreq();
            GABScript.addFreq();
        }
        else
        {
            GABScript.addFreq();
        }

        GABScript.checkMistakes = empty;

    }

    private void empty() { }
}
