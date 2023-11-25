using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [HideInInspector]
    public float damage;

<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        damage = 1f;
=======
    public LayerMask capaAIgnore;

    // Start is called before the first frame update
    void Start()
    {
        // Configurar capas de colisión
        int miCapa = gameObject.layer;

        // Configurar colisionadores
        Physics.IgnoreLayerCollision(miCapa, miCapa);
        Physics.IgnoreLayerCollision(miCapa, capaAIgnore);
        damage = 1f;
        Physics.IgnoreLayerCollision(miCapa, capaAIgnore);
>>>>>>> Stashed changes
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n es con el objeto objetivo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
<<<<<<< Updated upstream
        } else if (collision.gameObject.CompareTag("Player"))
=======
        }
        else if (collision.gameObject.CompareTag("Player"))
>>>>>>> Stashed changes
        {
            collision.gameObject.GetComponent<BoatController>().TakeDamage(damage);
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
