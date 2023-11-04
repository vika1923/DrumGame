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
        //StartCoroutine(blinkingBlackout());
    }

    IEnumerator blinkingBlackout() {
        for (int i = 1; i < 6; i++) {
            yield return new WaitForSeconds(i*0.2f);
            r.material.color = transparent;
            yield return new WaitForSeconds(4/i);
            r.material.color = black;
        }
    }
}