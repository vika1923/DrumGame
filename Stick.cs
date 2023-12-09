using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private int screenHeight;

    public List<Touch> touches = new List<Touch>();

    private void Start()
    {
        screenHeight = Screen.height / 4;
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++) {
            if (Input.touches[i].phase == TouchPhase.Began && Input.touches[i].position.y < screenHeight)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    if (hit.collider.TryGetComponent(out Tile tile))
                    {
                        tile.selfdestruct();
                    }
                }
            }
        }
        
    }
}
