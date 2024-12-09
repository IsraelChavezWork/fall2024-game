using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BuyWindow : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] GameObject buyCanvas;
    [SerializeField] GameObject DisplayCanvas;


    [Header("Money Type And Price")]
    [SerializeField] UnityEngine.UI.Image coinImage;
    [SerializeField] int price = 10;
    [SerializeField] bool priceIsPurpleCoins;

    [Header("Buttons")]
    [SerializeField] Button buyButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button checkoutButton;

    // All I do for good UI, now I know what the professor meant :\
    [Header("Other Buttons & Sprites")]
    [SerializeField] GameObject otherButton1;
    //[SerializeField] SpriteRenderer otherSprite1;
    [SerializeField] string otherItemName1;
    [SerializeField] GameObject otherButton2;
    //[SerializeField] SpriteRenderer otherSprite2;
    [SerializeField] string otherItemName2;
    [SerializeField] GameObject otherButton3;
    //[SerializeField] SpriteRenderer otherSprite3;
    [SerializeField] string otherItemName3;

    [Header("Item")]
    [SerializeField] string item;
    [SerializeField] GameObject foccusItem;
    [SerializeField] SpriteRenderer displayItem;

    [Header("Coins")]
    [SerializeField] int purpleCoins = 0;
    [SerializeField] int greenCoins = 0;


    void Awake()
    {
        //REMOVE CODE, ONLY FOR DEBUG:
        //PlayerPrefs.SetInt("GreenCoins", 100);
        //PlayerPrefs.SetInt("PurpleCoins", 100);
        //PlayerPrefs.SetInt("hasBuy" + item, 0);


        buyCanvas.SetActive(false);
        DisplayCanvas.SetActive(true);
        foccusItem.SetActive(false);
       
        
        bool hasBuyIt = Convert.ToBoolean(PlayerPrefs.GetInt("hasBuy" + item, 0));
        
        if(hasBuyIt){
           
            turnAllElementsOff();
        }

        greenCoins = PlayerPrefs.GetInt("GreenCoins", 0);
        purpleCoins = PlayerPrefs.GetInt("PurpleCoins", 0);
    }

    public void turnAllElementsOff(){
        

        buyCanvas.SetActive(false);
        DisplayCanvas.SetActive(false);

    }

    public void buy(){
        
        if(priceIsPurpleCoins){
            //do logic for purple coins
            if(purpleCoins < price){
                
                //do nothing or play some sound, lets do the sound lol
                return;
            }
            else{
                
                purpleCoins = PlayerPrefs.GetInt("PurpleCoins", 0);
                purpleCoins -= price;
                PlayerPrefs.SetInt("PurpleCoins", purpleCoins);
                restoreOtherButtons();
                purchase();
                
            }
        }
        else{
            //do logic for green coins
            if(greenCoins < price){
                //do nothing or play some sound, lets do the sound lol
            }
            else{
               
                greenCoins = PlayerPrefs.GetInt("GreenCoins", 0);
                greenCoins -= price;
                PlayerPrefs.SetInt("GreenCoins", greenCoins);
                restoreOtherButtons();
                purchase();
                
            }
        }

    }

    public void purchase(){
       
        PlayerPrefs.SetInt("hasBuy" + item, 1);
        turnAllElementsOff();
    }

    public void openShopDisplay(){
        //show element foccus
        buyCanvas.SetActive(true);
        DisplayCanvas.SetActive(false);
        foccusItem.SetActive(true);

        //disable the other checkout buttons
       
        otherButton1.SetActive(false);
        otherButton2.SetActive(false);
        otherButton3.SetActive(false);
    }

    public void closeShopDisplay(){ //meaning that we canceld, NOT BUY
        DisplayCanvas.SetActive(true);
        buyCanvas.SetActive(false);
        restoreOtherButtons();
    }

    public void restoreOtherButtons(){

       
        if(!Convert.ToBoolean(PlayerPrefs.GetInt("hasBuy" + otherItemName1, 0))){ //if they didn't buy that item
            otherButton1.SetActive(true);
        }

        if(!Convert.ToBoolean(PlayerPrefs.GetInt("hasBuy" + otherItemName2, 0))){ //if they didn't buy that item
            otherButton2.SetActive(true);
        }

        if(!Convert.ToBoolean(PlayerPrefs.GetInt("hasBuy" + otherItemName3, 0))){ //if they didn't buy that item
            otherButton3.SetActive(true);
        }

      

    }
}
