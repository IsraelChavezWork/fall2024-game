using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowJumpsLeft : MonoBehaviour
{
    [SerializeField] PlayerInputHandlerFINAL player;
    [SerializeField] TextMeshProUGUI jumpsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int jumps = player.GetPlayer().getJumps();
        string text = "Jumps Left: " + jumps.ToString();
        jumpsText.text = text;
    }
}
