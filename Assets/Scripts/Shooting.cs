using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject cannonball;
    public GameObject explotion;
    public Transform cannon;
    public AudioClip explotionSound;
    [HideInInspector]
    public targetAwareness _targetAwareness;

    public float force = 10f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _targetAwareness = GetComponent<targetAwareness>();
        InvokeRepeating("Shoot", 0f, 2f);
    }

    private void Shoot()
    {
        if (_targetAwareness.IsAware)
        {
            GameObject bullet = Instantiate(cannonball, cannon.position, cannon.rotation); //bola de cañon
            GameObject boom = Instantiate(explotion, cannon.position, cannon.rotation); //sonido
            if (explotionSound != null)
            {
                audioSource.PlayOneShot(explotionSound);
            }
            Vector3 targetPosition = _targetAwareness.ClosestTarget.position;
            Vector3 shootDirection = (targetPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = shootDirection * force;
            Destroy(bullet, 5);
            Destroy(boom, 1);
        }
    }
}
