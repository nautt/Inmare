using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PirateMap
{
    public class TreasureIslandBehaviour : MonoBehaviour
    {
        public TreasureIsland islandData;
        public int lootCounter;
        // [SerializeField] private TextMeshPro treasureCountText;
        public TextMeshProUGUI treasureCountText;
        private GameObject player;

        [Header("Loot prefabs")]
        public GameObject bronzeChest;
        public GameObject silverChest;
        public GameObject goldenChest;
        public GameObject rareChest;


        [Header("Loot Weights")]
        public float bronzeChestWeight = 4;
        public float silverChestWeight = 3;
        public float goldenChestWeight = 2;
        public float rareChestWeight = 1;
        private float totalWeight;

        private void Start()
        {
            islandData = new(this.transform.position);
            lootCounter = islandData.remainingLoot;
            player = GameObject.FindGameObjectWithTag("Player");
            totalWeight = silverChestWeight + bronzeChestWeight + goldenChestWeight + rareChestWeight;            

            silverChestWeight += bronzeChestWeight;
            goldenChestWeight += silverChestWeight;
            rareChestWeight += goldenChestWeight;
        }

        private void Update()
        {
            treasureCountText.text = "Treasures: " + lootCounter.ToString();
            LookAtActiveCamera(player);
        }


        //Spawnea cofres cada 5 segundos si el jugador entra al collider
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player"){
                InvokeRepeating("SpawnRandomChest", 0, 1f);
            }
        }

        //Cancela el spawn de cofres si el jugador sale del collider
        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                CancelInvoke();
            }
        }

        private void LookAtActiveCamera(GameObject player)
        {
            Camera playerMainCamera = player.transform.Find("Main Camera").GetComponent<Camera>();
            Camera playerSecondaryCamera = GameObject.Find("Secondary Camera").GetComponent<Camera>();

            if (playerMainCamera.enabled)
            {
                treasureCountText.transform.LookAt(playerMainCamera.transform);
            }
            else
            {
                treasureCountText.transform.LookAt(playerSecondaryCamera.transform);
            }
        }

        private void SpawnRandomChest()
        {
            if (lootCounter > 0) {
                GameObject chestInstance = RandomSpawner();
                chestInstance.transform.position = this.transform.position + new Vector3(0, .5f, 0);
                // chestInstance.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 200f);
                lootCounter--;
            }
        }

        private GameObject RandomSpawner()
        {   
            GameObject chestInstance;
            var dice = Random.Range(0, totalWeight);            
            if (dice >= 0 && dice < bronzeChestWeight)
                chestInstance = Instantiate(bronzeChest);
            else if (dice >= bronzeChestWeight && dice < silverChestWeight)
                chestInstance = Instantiate(silverChest);
            else if (dice >= silverChestWeight && dice < goldenChestWeight)
                chestInstance = Instantiate(goldenChest);
            else
                chestInstance = Instantiate(rareChest);
            
            return chestInstance;
        }
    }
}
