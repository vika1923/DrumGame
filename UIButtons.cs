using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    private GenerateAsyncBeat generateBeat;

    private void Start()
    {
        generateBeat = FindObjectOfType<GenerateAsyncBeat>();
    }

    public GameObject canvas;

    public void next() {
        canvas.SetActive(false);
        generateBeat.start();
        generateBeat.toInitValues();
        generateBeat.updating();
    }
    public void settings() {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
