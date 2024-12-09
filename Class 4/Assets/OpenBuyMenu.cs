using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBuyMenu : MonoBehaviour
{
    public void enableBuyWindow(){
        GetComponent<Canvas>().enabled = true;
    }

    public void disableBuyWindow(){
        GetComponent<Canvas>().enabled = false;
    }
}
