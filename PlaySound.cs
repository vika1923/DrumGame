using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public int score = 0;
    public int maxScore = 0;
    public int missedTilesNumber = 0;

    [SerializeField]
    GameObject RightStick;
    [SerializeField]
    GameObject LeftStick;

    MainScript MScript;
    Darken DScript;

    private bool extraSticks;

    public GameObject canvas;
   
    public int counter = 3;
    private int len;
    private int timeWithAllTiles = 5;

    public void start()
    {
        MScript = FindObjectOfType<MainScript>();
        DScript = FindObjectOfType<Darken>();

        extraSticks = MScript.extraTiles;

            if (extraSticks)
            {
                len = 5;
                LeftStick.SetActive(true);
                RightStick.SetActive(true);
            }
            else
            {
            len = 3;
            }
    }

    public void nextLevelRandomness(int currentLevel) {
        if (currentLevel >= len)
        {
            FindObjectOfType<GenerateAsyncBeat>().CancelInvoke("levelUp");
            countdown();
        }
    }
    public void nextLevelBlackout(int currentLevel)
    {
        if (currentLevel >= len)
        {
            Debug.Log("currentLevel >= len");
            FindObjectOfType<GenerateAsyncBeat>().CancelInvoke("levelUp");
            Invoke("lightsOff", 10);
        }
    }

    public void lightsOff() {
        FindObjectOfType<GenerateAsyncBeat>().startCheckingForMistakes();
        DScript.startBlinking();
        Invoke("lightsOn", timeWithAllTiles);
        countdown();
    }
    public void lightsOn() {
        DScript.lightsOn();
    }
    public void countdown() {
        Invoke("stopSpawning", timeWithAllTiles);
        Invoke("showGameOverScene", timeWithAllTiles+5);
    }
    void stopSpawning() {
        FindObjectOfType<GenerateAsyncBeat>().CancelInvoke("InvokeSpawnNextSet");
    }
    public void showGameOverScene() {
        canvas.SetActive(true);
        Debug.Log((float)score / maxScore * 100);

        //Debug.Log($"AFTER GAME: score: {FindObjectOfType<PlaySound>().score}; max score: {FindObjectOfType<PlaySound>().maxScore}");
    }
}
