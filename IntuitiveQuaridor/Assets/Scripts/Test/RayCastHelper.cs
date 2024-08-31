using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHelper : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        
        var raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
        Debug.Log(raycastHit2D.collider);
        
    }
}
