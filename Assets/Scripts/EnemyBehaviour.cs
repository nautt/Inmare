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
    public float damage = 1f;
    private targetAwareness _targetAwareness;
    public EnemyStates state;
    private Movement movement;

    [Header("Vida del enemigo")]
    [SerializeField] float health, maxhealth = 5f;
    [SerializeField] FloatingHealthbar healthbar;


    // Start is called before the first frame update
    void Awake()
    {
        _targetAwareness = GetComponent<targetAwareness>();
        state = EnemyStates.walking;
        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        health = maxhealth;
        healthbar.UpdateHealthBar(health, maxhealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

}
