using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class ItemCollector : MonoBehaviour
    {
        public AudioClip pickupSound;
        private void OnTriggerEnter(Collider collision)
        {
            var treasure = collision.GetComponent<Treasure>();
            if (treasure != null)
            {
                // Debug.Log("PickUp Treasure"); 
                if (this.name == "ChestSeller"){
                    treasure.Sell();
                }
                else
                {
                    treasure.Collect();
                }
                gameObject.GetComponent<AudioSource>().PlayOneShot(pickupSound, 0.5f);
            }
        }
    }
}