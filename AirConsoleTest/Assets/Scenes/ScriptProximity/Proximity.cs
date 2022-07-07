using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Proximity : MonoBehaviour
{
    public GameObject[] treasuries;
    public GameObject Player;
    public GameObject closestTreasure;
    public float Kalt = 45.0f;
    public float Warm = 25.0f;
    public float Hot = 8.0f;
    public GyroScript gyro;

    public int zustand; 

    void Start()
    {
        //Treasure = GameObject.FindWithTag("Treasure");
        Player = GameObject.FindWithTag("Player");
        treasuries = GameObject.FindGameObjectsWithTag("Treasure");

    }

    void Update()
    {
        closestTreasure = FindClosestTreasure();
        if(Vector3.Distance(Player.transform.position, closestTreasure.transform.position) < Kalt && Vector3.Distance(Player.transform.position, closestTreasure.transform.position) > Warm)
        {
            zustand = 1;
		}
        else if(Vector3.Distance(Player.transform.position, closestTreasure.transform.position) < Warm && Vector3.Distance(Player.transform.position, closestTreasure.transform.position) > Hot)
        {
            zustand = 2;
        }
        else if(Vector3.Distance(Player.transform.position, closestTreasure.transform.position) < Hot)
        {
            zustand = 3; 
        }
        else{
            zustand = 0;
        }
 
    }

    public GameObject FindClosestTreasure()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = Player.transform.position;
        foreach (GameObject treasure in treasuries)
            if (treasure == null){
            continue;
            }
            else 
        {
            Vector3 diff = treasure.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = treasure;
                distance = curDistance;
            }
        }
        return closest;
    }
}