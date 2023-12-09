using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    MainScript MScript;

    private void Awake()
    {
        MScript = FindObjectOfType<MainScript>();
    }

    public void showScore(float score) {

        int s = (int)Mathf.Round(score);
        scoreText.text = $"SCORE: {s}";

        if (MScript.extraTiles) {
            s *= (int)1.5;
        }
        s = (int)(s + (MScript.bpm * 2));
        s += 3*MScript.levelProgressionRate;

        if (PlayerPrefs.GetInt("gm", 1) == 0) {
            float bsr = PlayerPrefs.GetInt("bsr", 0);
            bestScoreText.text = $"Best Score: {bsr}";
            if (bsr < s) {
                PlayerPrefs.SetInt("bsr", s);
            }
        }
        else if (PlayerPrefs.GetInt("gm", 0) == 1) {
            float bsb = PlayerPrefs.GetInt("bsb", 0);
            bestScoreText.text = $"Best Score: {bsb}";
            if (bsb < s)
            {
                PlayerPrefs.SetInt("bsb", s);
            }
        }
    }
}
