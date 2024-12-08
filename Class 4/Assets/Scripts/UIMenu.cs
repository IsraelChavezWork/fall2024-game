using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] bool closeByDefault = true;

    void Awake()
    {
        if(closeByDefault){
            CloseMenu();
        }
    }
    public void OpenMcenu(){
        GetComponent<Canvas>().enabled=true;
    }

    public void CloseMenu(){
        GetComponent<Canvas>().enabled=false;
    }

    
}
