using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    GameObject RightStick;
    [SerializeField]
    GameObject LeftStick;

    MainScript MScript;

    private bool extraSticks;

    public GameObject canvas;
   
    public int counter = 3;
    private int len;

    public void start()
    {
        MScript = FindObjectOfType<MainScript>();

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

    public void nextLevel(int currentLevel) {
        if (currentLevel >= len)
        {
            FindObjectOfType<GenerateAsyncBeat>().CancelInvoke("nextLevel");
            countdown();
        }
    }

    public void countdown() {
        Invoke("stopSpawning", 25);
        Invoke("showGameOverScene", 30);
    }
    void stopSpawning() {
        FindObjectOfType<GenerateAsyncBeat>().CancelInvoke("spawnNextSet");
    }
    public void showGameOverScene() {
        canvas.SetActive(true);
    }
}
