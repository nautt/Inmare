using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;
    public GameObject player;

    private void Start()
    {
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        target.position = target.position + offset;
        LookAtActiveCamera(player);
    }

    private void LookAtActiveCamera(GameObject player)
    {
        Camera playerMainCamera = player.transform.Find("Main Camera").GetComponent<Camera>();
        Camera playerSecondaryCamera = GameObject.Find("Secondary Camera").GetComponent<Camera>();

        if (playerMainCamera.enabled)
        {
            transform.rotation = playerMainCamera.transform.rotation;
        }
        else
        {
            transform.rotation = playerSecondaryCamera.transform.rotation;
        }
    }
}