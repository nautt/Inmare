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
        private bool alternate = false;
        private bool storeOpen = false;
        private bool start = false;

        [Header("Loot prefabs")]
        public GameObject bronzeChest;
        public GameObject silverChest;
        public GameObject goldenChest;
        public GameObject rareChest;
        public GameObject shopWelcomeCanvas;
        public GameObject shopFarewellCanvas;
        public List<int> itemsInStore = new List<int>();
        GameObject[] storeCanvases;
        GameObject[] MainCanvases;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                itemsInStore.Add(Random.Range(0, 12));
            }
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
            playerMagnetArea = GameObject.Find("MagnetArea");
            playerCollector = GameObject.Find("CollectorCollider");
            shopWelcomeCanvas = GameObject.Find("ShopWelcome");
            shopFarewellCanvas = GameObject.Find("ShopFarewell");
            target = null;

            storeCanvases = GameObject.FindGameObjectsWithTag("StoreItem");
            if (storeCanvases.Length > 0)
            {
                for(int j = 0; j < storeCanvases.Length; j++)
                {
                    storeCanvases[j].GetComponent<ButtonInfo>().itemID = itemsInStore[j];
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (start == false)
            {
                start = true;
                shopWelcomeCanvas.SetActive(false);
                shopFarewellCanvas.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.F) && alternate == true && storeOpen == false)
            {
                storeCanvases = GameObject.FindGameObjectsWithTag("Store");
                MainCanvases = GameObject.FindGameObjectsWithTag("MainUI");
                shopWelcomeCanvas.SetActive(false);
                shopFarewellCanvas.SetActive(true);
                storeOpen = true;
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
            else if (Input.GetKeyDown(KeyCode.F) && alternate == true && storeOpen == true)
            {
                storeCanvases = GameObject.FindGameObjectsWithTag("Store");
                MainCanvases = GameObject.FindGameObjectsWithTag("MainUI");
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
                shopWelcomeCanvas.SetActive(true);
                shopFarewellCanvas.SetActive(false);
                storeOpen = false;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {                                
                playerMagnetArea.SetActive(false);
                playerCollector.SetActive(false);
                shopWelcomeCanvas.SetActive(true); 
                alternate = true;
                storeOpen = false;
                InvokeRepeating("SpawnNextChest", 0, 5f);
                

                storeCanvases = GameObject.FindGameObjectsWithTag("Store");
                MainCanvases = GameObject.FindGameObjectsWithTag("MainUI");
            }
        }


    

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                CancelInvoke();
                playerMagnetArea.SetActive(true);
                playerCollector.SetActive(true);
                shopWelcomeCanvas.SetActive(false);
                shopFarewellCanvas.SetActive(false);
                alternate = false;
                storeOpen = false;

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
