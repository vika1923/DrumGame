using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSettings : MonoBehaviour
{
    public TMP_Dropdown gamemode;
    public Slider bpmslider;
    public TMP_Text bpmvalue;
    public Slider lprslider;
    public TMP_Text lprvalue;

    public Toggle extraTiles;
    public Toggle glowing;

    private void Start()
    {
        bpmslider.onValueChanged.AddListener(delegate { displayNewBPM(); });
        lprslider.onValueChanged.AddListener(delegate { displayNewLPR(); });


        gamemode.value = PlayerPrefs.GetInt("gm");
        bpmslider.value = PlayerPrefs.GetInt("bpm")/30;
        lprslider.value = PlayerPrefs.GetInt("lpr");

        glowing.isOn = intToBool(PlayerPrefs.GetInt("glow"));
        extraTiles.isOn = intToBool(PlayerPrefs.GetInt("et"));

    }
    private void displayNewBPM() {
        bpmvalue.text = "("+ (bpmslider.value*30).ToString() +")";
    }
    private void displayNewLPR() {
        lprvalue.text = "(" + lprslider.value.ToString() + ")";
    }
    private int boolToInt(Toggle toggle) {
        if (toggle.isOn)
        {
            return 1;
        }
        else {
            return 0;
        }
    }
    private bool intToBool(int i)
    {
        if (i == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void saveChanges() {
        PlayerPrefs.SetInt("gm", gamemode.value);
        PlayerPrefs.SetInt("bpm", (int)bpmslider.value*30);
        PlayerPrefs.SetInt("lpr", (int)lprslider.value*2);
        PlayerPrefs.SetInt("glow", boolToInt(glowing));
        PlayerPrefs.SetInt("et", boolToInt(extraTiles));

        PlayerPrefs.Save();

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
