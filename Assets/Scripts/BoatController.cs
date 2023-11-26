﻿using PirateMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movimiendo del jugador")]
    public Transform Pivot;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 15f;

    public bool isAcorazado = false;

    public Camera mainCamera;
    public Camera secondaryCamera;

    protected Rigidbody rb;
    protected Quaternion Rotation;
    protected MapVisualizer visualizer;
    protected PlayerInventory inventory;

    [Header("Vida del jugador")]
    [SerializeField] public float health = 10f;
    [SerializeField] public float maxhealth = 10f;
    [SerializeField] FloatingHealthbar healthbar;

    [Header("Disparo")]
    public GameObject cannonball;
    public GameObject explotion;
    public Transform cannon_front;
    public Transform cannon_left;
    public Transform cannon_right;
    public AudioClip explotionSound;
    private AudioSource audioSource;
    public float force = 10f;

    [Header("Cooldown para disparar")]
    private float tiempoUltimoDisparo;
    public float tiempoCooldown = 1.0f; // Por ejemplo, un segundo de cooldown


    // Start 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Rotation = Pivot.localRotation;
        visualizer = GetComponentInParent<MapVisualizer>();
        inventory = GetComponent<PlayerInventory>();

        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
        audioSource = GetComponent<AudioSource>();

        mainCamera.enabled = true;

        secondaryCamera.enabled = false;
        secondaryCamera.GetComponent<AudioListener>().enabled = false;
    }

    void Update()
    {
        //Alternar camara
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondaryCamera.enabled = !secondaryCamera.enabled;
            if (secondaryCamera.enabled == true)
            {
                secondaryCamera.GetComponent<AudioListener>().enabled = true;
                mainCamera.GetComponent<AudioListener>().enabled = false;
            }
            else
            {
                secondaryCamera.GetComponent<AudioListener>().enabled = false;
                mainCamera.GetComponent<AudioListener>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            shoot(cannon_front); // Llama a la función de disparo
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            shoot(cannon_left); // Llama a la función de disparo
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            shoot(cannon_right); // Llama a la función de disparo
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }
    }
    void FixedUpdate()
    {
        var steer = 0;
        float slow = 1f;

        // Direcci�n de rotaci�n
        if (Input.GetKey(KeyCode.A))
        {
            steer = -1;
            slow = 0.5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = 1;
            slow = 0.5f;

        }

        float rotacion = steer * SteerPower * Time.deltaTime;

        // Rotamos el objeto en el eje vertical (y)
        transform.Rotate(Vector3.up, rotacion);

        // Calcular la direccion hacia la que mira el barco
        var forward = transform.forward;

        // Aplicar fuerza en la direccion hacia la que mira el barco cuando se acelera
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(forward * Power);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-forward * (Power / 2));
        }

        // Limitar la velocidad m�xima
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed * slow);
    }

    private void shoot(Transform cannon)
    {
        GameObject bullet = Instantiate(cannonball, cannon.position, cannon.rotation); //bola de cañon
        GameObject boom = Instantiate(explotion, cannon.position, cannon.rotation); //sonido
        if (explotionSound != null)
        {
            audioSource.PlayOneShot(explotionSound);
        }
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * force;
        Destroy(bullet, 5);
        Destroy(boom, 1);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Heal"))
        {
            heal();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.UpdateHealthBar(health, maxhealth);
        if (health <= 0)
        {
            inventory.nBronze = 0;
            inventory.nSilver = 0;
            inventory.nGodlen = 0;
            inventory.nRare = 0;
            inventory.nDabloons = 0;
            transform.position = visualizer.puntoInicio;
        }
    }

    public void heal()
    {
        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (isAcorazado == true && collision.gameObject.CompareTag("Enemy"))
            {
               collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(2f);
            }
        }
    }
}