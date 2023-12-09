using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLighting : MonoBehaviour
{
    // Make touch input work.
	public Light R;
	public Light CR;
	public Light C;
	public Light CL;
	public Light L;
	public Light mainLight;

    public AudioSource RS;
    public AudioSource CRS;
    public AudioSource CS;
    public AudioSource CLS;
    public AudioSource LS;

    GenerateAsyncBeat GABScript;

    private Color GreenLight = new Color(0.5f, 1, 0.5f);
	private Color RedLight = new Color(1, 0.5f, 0.5f);

    private void Start()
    {
        GABScript = FindObjectOfType<GenerateAsyncBeat>();
    }

    public void goRed(Light light) {
		light.color = RedLight;
        StartCoroutine(goBlack(light));
    }
    public void goGreen(Light light) {
        light.color = GreenLight;
		StartCoroutine(goBlack(light));
    }

	private IEnumerator goBlack(Light light) {
		yield return new WaitForSeconds(0.1f);
		light.color = Color.black;
    }

	public IEnumerator backout() {
        for (float i = 1; i >= 0; i -= 0.1f) {
            yield return new WaitForSeconds(0.2f);
            mainLight.color = new Color(i, i, i);
        }
        mainLight.color = Color.black;
    }

    public IEnumerator lightout()
    {
        for (float i = 0; i <= 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.2f);
            mainLight.color = new Color(i, i, i);
        }
        mainLight.color = Color.white;
    }

    public void instantBlackMainLight() {
        mainLight.color = Color.black;
        GABScript.checkMistakes += GABScript.checkMistakesWhenAllTiles;
    }
    public void manyMistakes() {
        mainLight.color = Color.white;
        GABScript.checkMistakes -= GABScript.checkMistakesWhenAllTiles;
        Invoke("instantBlackMainLight", 4);
    }
}
