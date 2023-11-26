using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace PirateMap
{
    public class Treasure : MonoBehaviour, ICollectible
    {
        // public static event Action OnTreasureCollected;
        Rigidbody rb;

        public TreasureType type;
        public int price;

        bool hasTarget;
        Vector3 targetPosition;
        public float moveSpeed = 1f;

        [HideInInspector] public GameObject player;

        [HideInInspector] public PlayerInventory inventory;

        public enum TreasureType
        {
            Bronze,
            Silver,
            Gold,
            Rare,
            Key
        }

        public Treasure(TreasureType type)
        {
            this.type = type;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
        }

        public void Collect()
        {
            Debug.Log("Treasure Collected");
            Destroy(gameObject);
            AddToInventory(this.type);
            //OnTreasureCollected?.Invoke();
        }

        public void Sell()
        {
            Debug.Log("Treasure Sold");
            Destroy(gameObject);
            inventory.nDabloons += price;
        }

        public void FixedUpdate()
        {
            if(hasTarget)
            {
                Vector3 targetDirection = (targetPosition - transform.position).normalized;
                rb.useGravity = true;
                rb.AddForce(new Vector3(0, 100f, 0));
                rb.velocity = new Vector3(targetDirection.x, targetDirection.y, targetDirection.z) * moveSpeed;
            }
        }

        public void SetTarget(Vector3 position)
        {
            targetPosition = position;
            hasTarget = true;
        }

        public void AddToInventory(TreasureType type)
        {
            Debug.Log("Adding to inventory (" + type + ")"); 
            switch (type)
            {
                case TreasureType.Bronze:
                    inventory.nBronze++;
                    break;
                case TreasureType.Silver:
                    inventory.nSilver++;
                    break;
                case TreasureType.Gold:
                    inventory.nGodlen++;
                    break;
                case TreasureType.Rare:
                    inventory.nRare++;
                    break;
                case TreasureType.Key:
                    inventory.hasKey = true;
                    break;
                default:
                    break;
            }
            inventory.isEmpty = false;
        }
    }
}
