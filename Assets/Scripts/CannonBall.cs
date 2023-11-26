using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("CannonBall")){
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }else if (gameObject.CompareTag("EnemyCannonball")){
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<BoatController>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}