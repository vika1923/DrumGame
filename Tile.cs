using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    PlaySound PScript;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        PScript = FindObjectOfType<PlaySound>();

        FindObjectOfType<PlaySound>().maxScore += 1;
        speed = FindObjectOfType<MainScript>().speed;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);

        if (transform.position.z < -25) {
            PScript.missedTilesNumber += 1;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        PScript.missedTilesNumber -= 1;
        PScript.score += 3;
        Destroy(gameObject);
    }
}
