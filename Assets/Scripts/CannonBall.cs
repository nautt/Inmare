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
        // Verifica si la colisi�n es con el objeto objetivo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
        } else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BoatController>().TakeDamage(damage);
        }
    }
}
