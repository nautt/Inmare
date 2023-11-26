using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [HideInInspector]
    public float damage=1f;
    private BoatController vidaJugador;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        vidaJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>();

        if (gameObject.CompareTag("CannonBall")){
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(vidaJugador.damage);
                Destroy(gameObject);
            }
        }else if (gameObject.CompareTag("EnemyCannonball")){
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<BoatController>().TakeDamage(vidaJugador.enemyDamage);
                Destroy(gameObject);
            }
        }
    }
}