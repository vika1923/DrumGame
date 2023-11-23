using System.Collections;
using UnityEngine;

public class Darken : MonoBehaviour
{
    Renderer r;

    Color black = new Color(0, 0, 0, 1);
    Color transparent = new Color(0, 0, 0, 0);

    private void Start()
    {
        r = GetComponent<Renderer>();
    }

    public void startBlinking() {
        StartCoroutine(blinkingBlackout());
    }
    public void lightsOff()
    {
        r.material.color = black;
    }
    public void lightsOn() {
        r.material.color = transparent;
    }

    IEnumerator blinkingBlackout() {
        for (int i = 1; i < 3; i++) {
            yield return new WaitForSeconds(0.5f);
            r.material.color = transparent;
            yield return new WaitForSeconds(0.3f);
            r.material.color = black;
        }
    }
}