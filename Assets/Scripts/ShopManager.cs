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
    public int [,] shopItems = new int[5,13];
    public TextMeshProUGUI DabloonsTxt;
    public List<Texture2D> items = new List<Texture2D>();
    // Usar dabloons como moneda


    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log(items.Count);
        // ID
        shopItems[1,1] = 1; // Tablones extra 1
        shopItems[1,2] = 2; // Tablones extra 2
        shopItems[1,3] = 3; // Tablones extra 3
        shopItems[1,4] = 4; // Rompe Ola
        shopItems[1,5] = 5; // Merc izq 1
        shopItems[1,6] = 6; // Merc izq 2
        shopItems[1,7] = 7; // Merc der 1
        shopItems[1,8] = 8; // Merc der 2
        shopItems[1,9] = 9; // canon mejor 1
        shopItems[1,10] = 10; // canon mejor 2
        shopItems[1,11] = 11; // canon mejor 3
        shopItems[1,12] = 12; // Barco Acorazado

        // Precio
        shopItems[2,1] = 1;
        shopItems[2,2] = 2;
        shopItems[2,3] = 3;
        shopItems[2,4] = 4;
        shopItems[2,5] = 5;
        shopItems[2,6] = 6;
        shopItems[2,7] = 7;
        shopItems[2,8] = 8;
        shopItems[2,9] = 9;
        shopItems[2,10] = 10;
        shopItems[2,11] = 11;
        shopItems[2,12] = 12;
        
        // Error en inventory
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        DabloonsTxt.text = "Dabloons:" + inventory.nDabloons;
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
