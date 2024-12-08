using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoor : MonoBehaviour
{

    [Header("Top Door")]
    [SerializeField] GameObject wallTop;
    [SerializeField] bool blockTop = false;

    [Header("Bottom Door")]
    [SerializeField] GameObject wallBottom;
    [SerializeField] bool blockBottom = false;

    [Header("Left Door")]
    [SerializeField] GameObject wallleft;
    [SerializeField] bool blockLeft = false;

    [Header("Right Door")]
    [SerializeField] GameObject wallRight;
    [SerializeField] bool blockRight = false;
    // Start is called before the first frame update
    void Start()
    {
        if(blockTop){
            wallTop.SetActive(true);
        }else{
            wallTop.SetActive(false);
        }

        if(blockBottom){
            wallBottom.SetActive(true);
        }else{
            wallBottom.SetActive(false);
        }

        if(blockLeft){
            wallleft.SetActive(true);
        }else{
            wallleft.SetActive(false);
        }

        if(blockRight){
            wallRight.SetActive(true);
        }else{
            wallRight.SetActive(false);
        }
    }

    
}
