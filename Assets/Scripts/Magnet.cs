using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class Magnet : MonoBehaviour
    {
        private void OnTriggerStay(Collider collision)
        {
                if (collision.gameObject.TryGetComponent<Treasure>(out Treasure treasure))
                {
                    //Debug.Log("Treasure detected");
                    treasure.SetTarget(transform.parent.position);
                }
        }
    }
}
