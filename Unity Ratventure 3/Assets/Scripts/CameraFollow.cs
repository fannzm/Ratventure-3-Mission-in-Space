using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referenz zum Spieler
    public Vector3 offset; // Offset zwischen Kamera und Spieler



    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

    }


}


