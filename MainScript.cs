using UnityEngine;

public class MainScript : MonoBehaviour
{
    public float speed;     //(of tile movement). calculate automaticly.

    [SerializeField]
    public float bpm;
    public float SpawnSpeed;

    [SerializeField]
    private GameObject tile0;
    [SerializeField]
    private GameObject tile1;
    [SerializeField]
    private GameObject tile2;
    [SerializeField]
    private GameObject tile3;
    [SerializeField]
    private GameObject tile4;

    public GameObject[] tiles;

    private GenerateAsyncBeat asyncBeat;
    private PlaySound playSound;

    public bool extraTiles;
    public int levelProgressionRate;


    void Start()
    {
        setFromPrefs();
        Application.targetFrameRate = 60;

        SpawnSpeed = 60 / bpm;
        tiles = new GameObject[] { tile0, tile1, tile2, tile3, tile4 };
        asyncBeat = FindObjectOfType<GenerateAsyncBeat>();
        playSound = FindObjectOfType<PlaySound>();
        playSound.start();
        asyncBeat.start();
        asyncBeat.updating();
    }
    private bool intToBool(int i) {
        if (i == 1)
        {
            return true;
        }
        else {
            return false;
        }
    }
    private void setFromPrefs() {
        levelProgressionRate = PlayerPrefs.GetInt("lpr", 4)*2;
        bpm = PlayerPrefs.GetInt("bpm", 120);
        extraTiles = intToBool(PlayerPrefs.GetInt("et", 1));

        //Debug.Log($"score: {FindObjectOfType<PlaySound>().score}; max score: {FindObjectOfType<PlaySound>().maxScore}");

        //Debug.Log($"play. || BPM:{bpm};  lpr:{levelProgressionRate};  eT:{PlayerPrefs.GetInt("et")}; gm:{PlayerPrefs.GetInt("gm")};");
    }
}
