using PirateMap;
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
    [SerializeField] public float damage = 1f;
    [SerializeField] FloatingHealthbar healthbar;

    [Header("Disparo")]
    public GameObject cannonball;
    public GameObject explotion;
    public Transform cannon_front;
    public Transform cannon_left;
    public Transform cannon_right;
    public AudioClip explotionSound;
    private AudioSource audioSource;

    private AudioSource musica;

    private AudioSource musicaBoss;
    public float force = 10f;
    public int doubleShootIzq = 1;
    public int doubleShootDer = 1;

    public GameObject yoMismo;

    [Header("Cooldown para disparar")]
    private float tiempoUltimoDisparo;
    public float tiempoCooldown = 1.0f; // Por ejemplo, un segundo de cooldown
    public GameObject shopWelcomeCanvas;
    private bool start = false;

    private bool flagsucia = true;

    public float enemyDamage = 1f;

    // Start 
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        musica = audioSources[1];
        musicaBoss = audioSources[2];
        musica.Play();

        Rotation = Pivot.localRotation;
        visualizer = GetComponentInParent<MapVisualizer>();
        inventory = GetComponent<PlayerInventory>();

        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
        audioSource = GetComponent<AudioSource>();

        mainCamera.enabled = false;

        secondaryCamera.enabled = true;
        secondaryCamera.GetComponent<AudioListener>().enabled = true;
        shopWelcomeCanvas = GameObject.Find("TextoBoss");
    }

    void Update()
    {
        //Alternar camara
        if (start == false)
        {
            start = true;
            shopWelcomeCanvas.SetActive(false);
        }


        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     mainCamera.enabled = !mainCamera.enabled;
        //     secondaryCamera.enabled = !secondaryCamera.enabled;
        //     if (secondaryCamera.enabled == true)
        //     {
        //         secondaryCamera.GetComponent<AudioListener>().enabled = true;
        //         mainCamera.GetComponent<AudioListener>().enabled = false;
        //     }
        //     else
        //     {
        //         secondaryCamera.GetComponent<AudioListener>().enabled = false;
        //         mainCamera.GetComponent<AudioListener>().enabled = true;
        //     }
        // }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            shoot(cannon_front, 0); // Llama a la función de disparo
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            for (int i = 0; i < doubleShootIzq; i++)
            {
                shoot(cannon_left, i);
            }
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time - tiempoUltimoDisparo >= tiempoCooldown)
        {
            for (int i = 0; i < doubleShootDer; i++)
            {
                shoot(cannon_right, i);
            }
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
        }

        if (Input.GetKeyDown(KeyCode.G) && inventory.nRare >= 1 && flagsucia)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            shopWelcomeCanvas.SetActive(false);
            jugador.transform.position = new Vector3(537, 0, 14);
            enemyDamage = 5f;
            flagsucia = false;

            musica.enabled = false;
            musicaBoss.enabled = true;
            musicaBoss.Play();
        }
        if (Input.GetKeyUp(KeyCode.G) && inventory.nRare >= 1)
        {
            shopWelcomeCanvas.SetActive(false);
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
            slow = 0.8f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = 1;
            slow = 0.8f;

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

    private void shoot(Transform cannon, int index)
    {
        Vector3 ofs = cannon.position + cannon.right*0.2f * index;
        GameObject bullet = Instantiate(cannonball, ofs, cannon.rotation); //bola de cañon
        if (cannon == cannon_front)
        {
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * force;
        }
        else if (cannon == cannon_left)
        {
            bullet.GetComponent<Rigidbody>().velocity = transform.right * force;
        }
        else if (cannon == cannon_right)
        {
            bullet.GetComponent<Rigidbody>().velocity = -transform.right * force;
        }
        Destroy(bullet, 3);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Heal"))
        {
            heal();
        }

        if (inventory.nRare >= 1 && flagsucia)
        {// TODO cambiar nrare por la weaita de llave
            shopWelcomeCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Heal") && flagsucia)
        {
            shopWelcomeCanvas.SetActive(false);
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

            if (musica.enabled == false){
                musicaBoss.enabled = false;
                musica.enabled = true;
                
            }                        
            musica.Play();        
            transform.position = visualizer.puntoInicio;
            doubleShootIzq = 1;
            doubleShootDer = 1;
            Power = 13;
            MaxSpeed = 4;
            enemyDamage = 1f;
            maxhealth = 10;
            flagsucia = true;
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
                collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(MaxSpeed/3);
            }
        }
    }
}