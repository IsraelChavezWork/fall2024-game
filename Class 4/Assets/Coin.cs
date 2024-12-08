using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Coin : MonoBehaviour
{

    //This scrip can work as a generic pick up script, sadly, my dumb dumb didn't see it in time unu
    [SerializeField] GameObject CoinGroupObjects;
    [SerializeField] SpriteRenderer coinSprite;
    [SerializeField] Light2D lightSource;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            CoinGroupObjects.SetActive(false);
        }
    }
}
