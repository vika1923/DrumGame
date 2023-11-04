
using UnityEngine;

public class Stick : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private string key;

    private Vector3 pos;

    private void Awake()
    {
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
        transform.position = new Vector3(pos.x, 0.1f, pos.z);
    }
}
