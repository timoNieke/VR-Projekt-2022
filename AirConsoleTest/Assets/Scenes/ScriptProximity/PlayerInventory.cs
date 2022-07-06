using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfChests { get; private set; }

    public UnityEvent<PlayerInventory> OnChestCollected;

    public void ChestCollected()
    {
        NumberOfChests++;
        OnChestCollected.Invoke(this);
    }
}