using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class targetAwareness : MonoBehaviour
{
    [HideInInspector]
    public bool IsAware { get; private set; }
    [HideInInspector]

    public Transform ClosestTarget { get; private set; }

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private float _searchRadius = 5f;

    /*
     *** En el Update() se hace el trabajo de detectar colisiones con la mascara mask en cada frame, si detecta colisiones, entonces
     *** se elegirá al objetivo más cercano para asignarle el ClosestTarget, tambien se asigna elol IsAware como true, simulando un estado
     *** de alerta
     */
    void Update()
    {
        Collider[] thiefsColliders = Physics.OverlapSphere(transform.position, _searchRadius, mask);
        if (thiefsColliders.Length > 0) {
            IsAware = true;
        } else {
            IsAware = false;
        }

        ClosestTarget = GetClosestTarget(thiefsColliders);

        if (ClosestTarget != null) Debug.DrawLine(transform.position, ClosestTarget.position, Color.red);
    }

    /*
     *** La función GetClosestTarget detectará todos los objetivos de la mascara que asignamos en mask, y de todos ellos
     *** elegirá al objeto que esté más cercano mediante un foreach, en este mismo proceso se diferencia si es un policia
     *** o no (variable necesaria para el Thief), retornara el Transform closestTarget como el objetivo más cercano.
     */
    Transform GetClosestTarget(Collider[] targets) {

        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var target in targets) {
            float distanceToTarget = (target.transform.position - currentPosition).sqrMagnitude; //Raiz cuadrada es muy cara por lo que la evitamos
                                                                                                 //Solo interesa comparar distancias.
            if (distanceToTarget < closestDistance) {
                closestDistance = distanceToTarget;
                closestTarget = target.transform;
            }
        }       

        return closestTarget;
    }
}
