
using UnityEngine;

public class Stick : MonoBehaviour
{
    PlaySound PScript;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private string key;

    private Vector3 pos;

    private void Awake()
    {
        PScript = FindObjectOfType<PlaySound>();
        pos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key)) {
            PlayClip();
        }   
    }

    void PlayClip()
    {
        transform.position = new Vector3(pos.x, 0.5f, pos.z);
        Invoke("goDown", 0.15f);
        audioSource.Play();
    }
    private void goDown() {
        PScript.score -= 2;
        transform.position = new Vector3(pos.x, 0.1f, pos.z);
    }
}
