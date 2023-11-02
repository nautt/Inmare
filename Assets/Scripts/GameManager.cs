using System.Collections;
using System.Collections.Generic;
using PirateMap;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinsObjective;
    private PlayerInventory inventory;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.nDabloons >= coinsObjective)
        {
            Debug.Log("Game finished");
            Application.Quit();
            // UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
