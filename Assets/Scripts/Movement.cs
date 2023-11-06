using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float _maxSpeed = 3f;

    [SerializeField]
    private float _minSpeed = 1f;

    [SerializeField]
    private float _rotationSpeed = 3f;

    [SerializeField]
    private float _obstacleDetectionDistance = 1f;

    [HideInInspector]
    public targetAwareness _targetAwareness;

    [HideInInspector]
    public Vector3 _targetDirection;

    private float _speed;
    private float _timeUntilDirectionChange;

    private void Awake() {
        _targetAwareness = GetComponent<targetAwareness>();
        _targetDirection = transform.forward;
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update() {
        MoveForward();
    }

    private void FixedUpdate()
    {
        RotateTowardsRandom();
        RotateTowardsTarget();
        ObstacleDetection();
    }

    /*
    *** Establece valor de direccion aleatorea a _targetDirection entre los -90° y 90°,
    *** en intervalos de tiempo aleatoreos entre 1s y 5s.
    */
    private void RotateTowardsRandom(){
        _timeUntilDirectionChange -= Time.deltaTime;

        if (_timeUntilDirectionChange <= 0) {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.up);

            //Quaternion * Vector3 => Aplicar rotacion a targetDirection
            _targetDirection = rotation * _targetDirection;

            _timeUntilDirectionChange = Random.Range(1f, 5f); //Rango entre 1 a 5 seg.
        }
    }

    public void StartWalking()
    {
        RotateTowardsRandom();
    }

    /*
    *** Utiliza Quaternions para rotar el objeto en la direccion a la que esté establecida _targetDirection.
    */
    private void RotateTowardsTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    /*
    *** Traslada la entidad hacia adelante con una velocidad _speed.
    */
    private void MoveForward() {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    /*
    *** Utiliza 2 raycast para detectar obstáculos en el camino y determinar a que direccion girar para evitarlos.
    */
    private void ObstacleDetection() {
        float angleChange;

        bool rayCastLeft = Physics.Raycast(transform.position + transform.right*-.2f, transform.forward, out RaycastHit hitLeft, _obstacleDetectionDistance, LayerMask.GetMask("Obstacle"));
        bool rayCastRight = Physics.Raycast(transform.position + transform.right*.2f, transform.forward, out RaycastHit hitRight, _obstacleDetectionDistance, LayerMask.GetMask("Obstacle"));

        if (rayCastLeft && rayCastRight) {
            //Debug.Log("Raycast Left: " + hitLeft.distance);
            //Debug.Log("Raycast Right: " + hitRight.distance);
            Debug.DrawRay(transform.position + transform.right*-.2f, transform.forward * hitLeft.distance, Color.red);
            Debug.DrawRay(transform.position + transform.right*.2f, transform.forward * hitRight.distance, Color.green);
            if (hitLeft.distance > hitRight.distance) {
                angleChange = -10f;
            } else {
                angleChange = 10f;
            }

            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.up);

            //Quaternion * Vector3 => Aplicar rotacion a targetDirection
            _targetDirection = rotation * _targetDirection;
        }
    }

    /*
    ***Puede ser accedida por otros scripts para controlar el movimiento del gameObject.
    ***(Debe ser utilizada dentro de Update o FixedUpdate)
    */
    public void changeDirection(Vector3 newDirection) {
        _targetDirection = newDirection - transform.position;
    }
}
