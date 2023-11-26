using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float movSmooth, rotSmooth;
    public float angleOffset = 45f;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, movSmooth * Time.deltaTime);

        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the angle offset
        rotation *= Quaternion.Euler(angleOffset, 0, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmooth * Time.deltaTime);
    }
}
