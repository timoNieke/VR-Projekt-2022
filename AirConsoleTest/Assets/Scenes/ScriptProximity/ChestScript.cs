using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public AudioClip chestSound;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.ChestCollected();
            AudioSource.PlayClipAtPoint(chestSound, transform.position);
            
            //deaktivert die Chest, jedoch werden die "Temperatur Logs" weitergezählt
            //gameObject.SetActive(false);

            //zerstört das Objekt, jedoch wird für die nächsten Chests die "Temperatur Logs" nicht weitergezählt
            Destroy(gameObject);
        }
    }
}