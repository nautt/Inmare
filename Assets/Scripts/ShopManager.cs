using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PirateMap;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class ShopManager : MonoBehaviour
{

    private PlayerInventory inventory;
    private BoatController vidaJugador;

    private CannonBall danioJugador;
    //private EnemyBehaviour[] danioJugador;

    // cantidad de items a mostrar
    public float[,] shopItems = new float[3, 12];
    public TextMeshProUGUI DabloonsTxt;
    public List<Texture2D> items = new List<Texture2D>();
    // Usar dabloons como moneda


    // Start is called before the first frame update
    void Start()
    {
        // ID
        shopItems[1, 0] = 0.3f; // Tablones extra 1
        shopItems[1, 1] = 0.5f; // Tablones extra 2
        shopItems[1, 2] = 1f; // Tablones extra 3
        shopItems[1, 3] = 3f; // Rompe Ola
        shopItems[1, 4] = 4f; // Merc izq 1
        shopItems[1, 5] = 5f; // Merc izq 2
        shopItems[1, 6] = 6f; // Merc der 1
        shopItems[1, 7] = 7f; // Merc der 2
        shopItems[1, 8] = 0.3f; // canon mejor 1
        shopItems[1, 9] = 0.5f; // canon mejor 2
        shopItems[1, 10] = 1f; // canon mejor 3
        shopItems[1, 11] = 11f; // Barco Acorazado

        // Precio
        shopItems[2, 0] = 1f;
        shopItems[2, 1] = 2f;
        shopItems[2, 2] = 3f;
        shopItems[2, 3] = 4f;
        shopItems[2, 4] = 5f;
        shopItems[2, 5] = 6f;
        shopItems[2, 6] = 7f;
        shopItems[2, 7] = 8f;
        shopItems[2, 8] = 9f;
        shopItems[2, 9] = 10f;
        shopItems[2, 10] = 11f;
        shopItems[2, 11] = 12f;

        //DabloonsTxt.text = "Dabloons:" + 
    }

    void Update()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        DabloonsTxt.text = "Dabloons:" + inventory.nDabloons.ToString();
    }



    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (inventory.nDabloons >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])
        {
            // Logica de compra de Item
            inventory.nDabloons -= (int)shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];

            vidaJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>();
            danioJugador = vidaJugador.cannonball.GetComponent<CannonBall>();
            //danioJugador = GameObject.FindGameObjectsWithTag("Enemy").Select(e => e.GetComponent<EnemyBehaviour>()).Where(e => e != null).ToArray();

            DabloonsTxt.text = "Dabloons:" + inventory.nDabloons.ToString();
            // este es para aumentar daño
            if (ButtonRef.GetComponent<ButtonInfo>().itemID == 8 || ButtonRef.GetComponent<ButtonInfo>().itemID == 9 || ButtonRef.GetComponent<ButtonInfo>().itemID == 10){
                danioJugador.damage *= (1f + shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID]);
/*
                foreach (EnemyBehaviour obj in danioJugador)
                    {                        
                        danioJugador.damage *= (1f + shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID]);
                    }
*/
            }
            //este es para aumentar la vida
            else if (ButtonRef.GetComponent<ButtonInfo>().itemID == 0 || ButtonRef.GetComponent<ButtonInfo>().itemID == 1 || ButtonRef.GetComponent<ButtonInfo>().itemID == 2)
            {
                vidaJugador.maxhealth *= (1f + shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID]);
            }
            else if (ButtonRef.GetComponent<ButtonInfo>().itemID == 3)
            {
                vidaJugador.Power *= 1.3f;
            }
             else if (ButtonRef.GetComponent<ButtonInfo>().itemID == 11) //barco acorazado
            {
              vidaJugador.isAcorazado = true;
            }
        }
    }
}
