using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int itemID;
    public TextMeshProUGUI PriceTxt;
    public RawImage rawImageComponent;
    public GameObject ShopManager;

    // Cambiar a apertura de tiendas
    void Update()
    {
        PriceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[2, itemID].ToString();
        rawImageComponent.texture = ShopManager.GetComponent<ShopManager>().items[itemID];
    }

}
