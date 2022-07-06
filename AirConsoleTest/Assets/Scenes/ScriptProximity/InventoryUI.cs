using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI ChestText;

    // Start is called before the first frame update
    void Start()
    {
        ChestText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateChestText(PlayerInventory playerInventory)
    {
        ChestText.text = playerInventory.NumberOfChests.ToString();
    }
}
