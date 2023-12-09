using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutGMode : MonoBehaviour
{
    GenerateAsyncBeat GABScript;
    MainScript MScript;

    public List<int> offsets;

    private void Start()
    {
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
        MScript = FindObjectOfType<MainScript>();

        offsets = new List<int> { };
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

        addOffsets(GABScript.frequences);

        GABScript.checkMistakes -= GABScript.checkMistakesWhenAllTiles;

    }

    private void addOffsets(List<int> frequences)
    {
        List<int> possibleOffsets = new List<int> { 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7 }; // 13
        for (int i = 0; i < frequences.Count; i++)
        {
            int randomIndex = 0;
            if (frequences[i] > 7)
            {
                randomIndex = Random.Range(0, 13);
            }
            else if (frequences[i] > 3)
            {
                randomIndex = Random.Range(0, 5);
            }

            int randomOffset = possibleOffsets[randomIndex];
            offsets.Add(randomOffset);
        }
    }
}
