using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCoins : MonoBehaviour
{
    [SerializeField] PlayerInputHandlerFINAL player;
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int greenCoins = player.GetPlayer().getCoins()[0];
        int prupleCoins = player.GetPlayer().getCoins()[1];
        string text = greenCoins.ToString() + "\n\n" + prupleCoins.ToString();
        
        coinsText.text = text;
    }
}
