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
<<<<<<< Updated upstream
=======
    private BoatController vidaJugador;

    private CannonBall danioJugador;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            inventory.nDabloons -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            DabloonsTxt.text = "Dabloons:" + inventory.nDabloons;
=======
            inventory.nDabloons -= (int)shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];

            vidaJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>();
            danioJugador = vidaJugador.cannonball.GetComponent<CannonBall>();

            DabloonsTxt.text = "Dabloons:" + inventory.nDabloons.ToString();
            // este es para aumentar daño
            if (ButtonRef.GetComponent<ButtonInfo>().itemID == 8 || ButtonRef.GetComponent<ButtonInfo>().itemID == 9 || ButtonRef.GetComponent<ButtonInfo>().itemID == 10){ 
                danioJugador.damage *= (1f + shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID]);

            }//este es para aumentar la vida
            else if (ButtonRef.GetComponent<ButtonInfo>().itemID == 0 || ButtonRef.GetComponent<ButtonInfo>().itemID == 1 || ButtonRef.GetComponent<ButtonInfo>().itemID == 2)
            {
                vidaJugador.maxhealth *= (1f + shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID]);
            }
            else if (ButtonRef.GetComponent<ButtonInfo>().itemID == 3)
            {
                vidaJugador.Power *= 1.3f;
            }
>>>>>>> Stashed changes
        }
    }
}
