using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    Collider[] InsideZone;

    private void fixedUpdate()
    {
        InsideZone = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider collider in InsideZone)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("Player is in the zone");
            }
        }
    }
}
