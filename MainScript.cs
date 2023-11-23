using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SocialPlatforms.Impl;

public class MainScript : MonoBehaviour
{
    public float speed;     //(of tile movement). calculate automaticly.

    [SerializeField]
    private float bpm;
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

    [SerializeField]
    private PostProcessLayer PPLayer;

    public GameObject[] tiles;

    private GenerateAsyncBeat asyncBeat;
    private PlaySound playSound;

    public bool extraTiles;
    public int levelProgressionRate;


    void Start()
    {
        if (!PlayerPrefs.HasKey("bpm"))
        {
            PlayerPrefs.SetInt("bpm", 120);
            PlayerPrefs.SetInt("lpr", 4);
            PlayerPrefs.SetInt("et", 1);
            PlayerPrefs.SetInt("glow", 1);
            PlayerPrefs.Save();
        }
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
        levelProgressionRate = PlayerPrefs.GetInt("lpr")*2;
        bpm = PlayerPrefs.GetInt("bpm");
        extraTiles = intToBool(PlayerPrefs.GetInt("et"));

        if (intToBool(PlayerPrefs.GetInt("glow")))
        {
            PPLayer.enabled = true;
        }
        else {
            PPLayer.enabled = false;
        }

        //Debug.Log($"score: {FindObjectOfType<PlaySound>().score}; max score: {FindObjectOfType<PlaySound>().maxScore}");

        //Debug.Log($"play. || BPM:{bpm};  lpr:{levelProgressionRate};  eT:{PlayerPrefs.GetInt("et")}; gm:{PlayerPrefs.GetInt("gm")};");
    }
}
