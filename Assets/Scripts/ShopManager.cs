using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PirateMap;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{

    private PlayerInventory inventory;

    // cantidad de items a mostrar
    public int [,] shopItems = new int[5,5];
    public TextMeshProUGUI DabloonsTxt;
    // Usar dabloons como moneda


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        DabloonsTxt.text = "Dabloons:" + inventory.nDabloons;

        // ID
        shopItems[1,1] = 1;

        // Precio
        shopItems[2,1] = 5;

    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (inventory.nDabloons  >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]){
            // Logica de compra de Item
            inventory.nDabloons -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            DabloonsTxt.text = "Dabloons:" + inventory.nDabloons;
        }
    }
}
