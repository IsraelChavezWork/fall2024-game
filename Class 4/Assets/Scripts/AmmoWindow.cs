using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class AmmoWindow : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TextMeshProUGUI ammoText;

    [Header("Reload Bar")]
    [SerializeField] RectTransform barTransform;
    [SerializeField] List<GameObject> hidableBarObjects;
    // Start is called before the first frame update

    void Start()
    {
        //Debug.Log(playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetAmmo());
    }

    void SetReloadProgress(float progress){
        barTransform.localScale = new Vector3(progress, barTransform.localScale.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        int ammoAmount = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetAmmo();
        int maxAmmo = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetMaxAmmo();
        float fraction = ((float)ammoAmount / (float)maxAmmo);

        ammoText.text = ammoAmount.ToString();
        ammoText.color = Color.Lerp(Color.yellow, Color.white, fraction); //from white to yellow
        if(ammoAmount == 0){
            ammoText.color = Color.red;
        }

        float reloadProgress = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetReloadPercentage();
        SetReloadProgress(reloadProgress);
        if(reloadProgress <= 0){
            foreach(GameObject g in hidableBarObjects){
                g.SetActive(false); //this will remove the eleent from the game, however, if we have the reference, we can activaite it later
            }
        }else{
            foreach(GameObject g in hidableBarObjects){
                g.SetActive(true); //this will add the element from the game, however, if we have the reference, we can hide it later
            }
        }
        
        /*
        if(ammoAmount > 0){
            ammoText.color = Color.white;
        }else{
            ammoText.color = Color.red;
        }
        */
    }
}
