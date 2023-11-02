using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown("left shift")) {
            speed = 12f;
        } else if (Input.GetKeyUp("left shift")) {
            speed = 6f;
        }

        if (direction.magnitude >= 0.1f) {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
