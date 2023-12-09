using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public int score = 0;
    public int maxScore = 0;

    MainScript MScript;
    GenerateAsyncBeat GABScript;
    ManageLighting MLScript;

    private bool extraSticks;

    public GameObject canvas;
   
    public int counter = 3;
    private int len;
    private int timeWithAllTiles;

    public void start()
    {
        MScript = FindObjectOfType<MainScript>();
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
        MLScript = FindObjectOfType<ManageLighting>();

        timeWithAllTiles = 15 + MScript.levelProgressionRate * 2;

        extraSticks = MScript.extraTiles;

            if (extraSticks)
            {
                len = 5;
            }
            else
            {
            len = 3;
            }
    }

    public void nextLevelRandomness(int currentLevel) {
        if (currentLevel >= len)
        {
            GABScript.CancelInvoke("levelUp");
            countdown();
        }
    }
    public void nextLevelBlackout(int currentLevel)
    {
        if (currentLevel >= len)
        {
            GABScript.CancelInvoke("levelUp");
            Invoke("lightsOff", 5+2*MScript.levelProgressionRate);
        }
    }

    private void lightsOff() {
        Invoke("lightsOn", timeWithAllTiles);
        GABScript.checkMistakes += GABScript.checkMistakesWhenAllTiles;
        StartCoroutine(MLScript.backout());
        countdown();
    }
    public void lightsOn() {
        StartCoroutine(MLScript.lightout());
        GABScript.checkMistakes -= GABScript.checkMistakesWhenAllTiles;
    }
    public void countdown() {
        Invoke("stopSpawning", timeWithAllTiles);
        Invoke("showGameOverScene", timeWithAllTiles + 5);
    }
    void stopSpawning() {
        GABScript.CancelInvoke("InvokeSpawnNextSet");
    }
    public void showGameOverScene() {
        canvas.SetActive(true);

        FindObjectOfType<DisplayScore>().showScore((float)score / maxScore * 100);
    }
}
