using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    public GameObject Treasure;
    public GameObject Player;
    public float Kalt = 45.0f;
    public float Warm = 25.0f;
    public float Hot = 8.0f;



    void Start()
    {
        Treasure = GameObject.FindWithTag("Treasure");
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Kalt && Vector3.Distance(Player.transform.position, Treasure.transform.position) > Warm)
        {
            Debug.Log("Kalt");
        }
        else if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Warm && Vector3.Distance(Player.transform.position, Treasure.transform.position) > Hot)
        {
            Debug.Log("Warm");
        }
        else if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Hot)
        {
            Debug.Log("Hot");
        }
 
    }
}