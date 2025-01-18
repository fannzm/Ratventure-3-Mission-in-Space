using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referenz zum Spieler
    public Vector3 offset; // Offset zwischen Kamera und Spieler
    public float minY = -5f; // Untere Begrenzung der Y-Achse
    public float maxY = 5f;  // Obere Begrenzung der Y-Achse


    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        
    }

    
}


