using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowLifes : MonoBehaviour
{
    [SerializeField] PlayerInputHandlerFINAL player;
    [SerializeField] TextMeshProUGUI lifeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int lifes = player.GetPlayer().getLifes();
        int maxLifes = player.GetPlayer().getMaxLifes();
        String text = "Lifes Remaining: " + lifes.ToString() +"/" + maxLifes.ToString();
        lifeText.text = text;
    }
}
