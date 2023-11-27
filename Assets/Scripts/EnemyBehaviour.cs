using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[HideInInspector]
public enum EnemyStates
{
    walking,
    attacking
}

public class EnemyBehaviour : MonoBehaviour
{

    [HideInInspector]
    
    private targetAwareness _targetAwareness;
    public EnemyStates state;
    private Movement movement;
    public float stopDistance = 1f;
    private float original_speed;

    [Header("Vida del enemigo")]
    [SerializeField] float health, maxhealth = 5f;
    [SerializeField] FloatingHealthbar healthbar;

    [SerializeField] public float damage = 1f;


    // Start is called before the first frame update
    void Awake()
    {
        _targetAwareness = GetComponent<targetAwareness>();
        state = EnemyStates.walking;
        movement = GetComponent<Movement>();
        original_speed = movement._speed;
    }

    private void Start()
    {
        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
        // InvokeRepeating("Buff",120f, 120f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        if (_targetAwareness.IsAware) 
        {
            state = EnemyStates.attacking;
        } else
        {
            state = EnemyStates.walking;
        }

        if (state == EnemyStates.walking)
        {
            movement.StartWalking();
        }
        if (state == EnemyStates.attacking)
        {
            movement.changeDirection(_targetAwareness.ClosestTarget.position);
            if (Vector3.Distance(_targetAwareness.ClosestTarget.transform.position, this.transform.position) < stopDistance)
            {
                movement._speed = 0f;
            } else {
                movement._speed = original_speed;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.UpdateHealthBar(health, maxhealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }    

    // public void Buff(){
    //     maxhealth += maxhealth*.5f;
    //     health = maxhealth;
    //     healthbar.UpdateHealthBar(health, maxhealth);
    // }
}
