using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class SellTreasure : MonoBehaviour
    {
        [HideInInspector] public GameObject player;
        private PlayerInventory inventory;
        private GameObject playerMagnetArea;
        private GameObject playerCollector;
        private GameObject target;

        [Header("Loot prefabs")]
        public GameObject bronzeChest;
        public GameObject silverChest;
        public GameObject goldenChest;
        public GameObject rareChest;
        GameObject[] storeCanvases;
        GameObject[] MainCanvases;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
            playerMagnetArea = GameObject.Find("MagnetArea");
            playerCollector = GameObject.Find("CollectorCollider");
            target = null;
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // private void OnTriggerStay(Collider collision)
        // {
        //         if (collision.gameObject.CompareTag("Store"))
        //         {
        //             //Debug.Log("Treasure detected");

        //             //Instanciar cofres
        //             //treasure.SetTarget(transform.parent.position);
        //         }
        // }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {                
                Debug.Log("aqui hay que hacer la weaita de la tienda");
                playerMagnetArea.SetActive(false);
                playerCollector.SetActive(false);
                // target = collision.gameObject;
                InvokeRepeating("SpawnNextChest", 0, 5f);

                storeCanvases = GameObject.FindGameObjectsWithTag("Store");
                MainCanvases = GameObject.FindGameObjectsWithTag("MainUI");
                //Aquí llamar canva de tienda
                if (storeCanvases.Length > 0 && MainCanvases.Length > 0)
                {
                    Canvas storeCanvas = storeCanvases[0].GetComponent<Canvas>();
                    Canvas MainCanvas = MainCanvases[0].GetComponent<Canvas>();
                    if (storeCanvas != null && MainCanvas != null)
                    {
                        storeCanvas.enabled = true;
                        MainCanvas.enabled = false;
                    }
                }
            }
        }

        
        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                CancelInvoke();
                playerMagnetArea.SetActive(true);
                playerCollector.SetActive(true);

                storeCanvases = GameObject.FindGameObjectsWithTag("Store");
                MainCanvases = GameObject.FindGameObjectsWithTag("MainUI");
                //Aquí llamar canva de tienda
                if (storeCanvases.Length > 0 && MainCanvases.Length > 0)
                {
                    Canvas storeCanvas = storeCanvases[0].GetComponent<Canvas>();
                    Canvas MainCanvas = MainCanvases[0].GetComponent<Canvas>();
                    if (storeCanvas != null && MainCanvas != null)
                    {
                        storeCanvas.enabled = false;
                        MainCanvas.enabled = true;
                    }
                }
            }
        }

        private void SpawnNextChest()
        {
            GameObject chestInstance = null;

            if(inventory.nBronze > 0)
            {
                chestInstance = Instantiate(bronzeChest);
                inventory.nBronze--;
            }
            else if (inventory.nSilver > 0)
            {
                chestInstance = Instantiate(silverChest);
                inventory.nSilver--;
            }
            else if (inventory.nGodlen > 0)
            {
                chestInstance = Instantiate(goldenChest);
                inventory.nGodlen--;
            }
            else if (inventory.nRare > 0)
            {
                chestInstance = Instantiate(rareChest);
                inventory.nRare--;
            }
            else
            {
                Debug.Log("No quedan mas cofres por vender");
                inventory.isEmpty = true;
            }

            chestInstance.transform.position = player.transform.position + new Vector3(0, .5f, 0);
            if (chestInstance.gameObject.TryGetComponent<Treasure>(out Treasure treasure))
                {
                    //Debug.Log("Treasure detected");
                    treasure.SetTarget(this.transform.position);
                }

        }
    }
}
