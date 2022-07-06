using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Proximity : MonoBehaviour
{
    public GameObject Treasure;
    public GameObject Player;
    public float Kalt = 45.0f;
    public float Warm = 25.0f;
    public float Hot = 8.0f;
    public GyroScript gyro;

    public int zustand; 


    void Start()
    {
        Treasure = GameObject.FindWithTag("Treasure");
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Treasure != null)
        {
            if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Kalt && Vector3.Distance(Player.transform.position, Treasure.transform.position) > Warm)
            {
                zustand = 1;
                Debug.Log("Kalt");
            }
            else if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Warm && Vector3.Distance(Player.transform.position, Treasure.transform.position) > Hot)
            {
                zustand = 2;
                Debug.Log("Warm");
            }
            else if(Vector3.Distance(Player.transform.position, Treasure.transform.position) < Hot)
            {
                zustand = 3; 
                Debug.Log("Heiß");
            }
            else{
                zustand = 0;
            }
        }

    }
}