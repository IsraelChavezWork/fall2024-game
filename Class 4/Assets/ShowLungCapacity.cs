using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ShowLungCapacity : MonoBehaviour
{
    [SerializeField] PlayerInputHandlerFINAL player;
    [SerializeField] TextMeshProUGUI lungCapacityText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float lungCapacity = player.GetPlayer().getLungCapacity();
        lungCapacity = (float)Math.Round(lungCapacity, 1);
        String text = "Lung Capacity: " + lungCapacity.ToString();
        lungCapacityText.text = text;
    }
}
