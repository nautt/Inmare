using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PirateMap
{
    public class PlayerInventory : MonoBehaviour
    {
        public int nBronze;
        public int nSilver;
        public int nGodlen;
        public int nRare;
        public bool hasKey;
        public int nDabloons;

        public TextMeshProUGUI BronzeText;
        public TextMeshProUGUI SilverText;
        public TextMeshProUGUI GoldenText;
        public TextMeshProUGUI RareText;
        public TextMeshProUGUI CoinsText;
        public TextMeshProUGUI keyText;

        [HideInInspector] public bool isEmpty;


        void Start()
        {
            nBronze = 0;
            nSilver = 0;
            nGodlen = 0;
            nRare = 0;
            nDabloons = 1000;
            hasKey = false;
            isEmpty = true;
        }

        void Update()
        {
            BronzeText.text = "Bronze: " + nBronze.ToString();
            SilverText.text = "Silver: " + nSilver.ToString();
            GoldenText.text = "Gold: " + nGodlen.ToString();
            RareText.text = "Rare: " + nRare.ToString();
            CoinsText.text = "Dabloons: " + nDabloons.ToString();

            if(hasKey) keyText.text = "¡Tienes la llave!";
        }
    }
}
