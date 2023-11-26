using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PirateMap
{
    public class KeyIslandBehaviour : MonoBehaviour
    {
        public TreasureIsland islandData;
        private GameObject player;
        public GameObject key;
        public bool spawnKey;

        private void Start()
        {
            islandData = new(this.transform.position);
            player = GameObject.FindGameObjectWithTag("Player");
            spawnKey = true;
        }

        //Spawnea cofres cada 5 segundos si el jugador entra al collider
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player" && spawnKey){
                SpawnKey();
                spawnKey = false;
            }
        }

        private void SpawnKey()
        {
                GameObject keyInstance = Instantiate(key);
                keyInstance.transform.position = this.transform.position + new Vector3(0, .5f, 0);
                keyInstance.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 200f);
        }

    }
}
