using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color visitColor;
    [SerializeField] Color colonizedColor;
    SpriteRenderer spriteRenderer;

    [SerializeField] int visitors = 0;

    [Header("Colonization")]
    [SerializeField] Transform colonizeProgressTransform;
    [SerializeField] SpriteRenderer colonizeProgressSpriteRenderer;
    [SerializeField] float colonizeTime = 1;
    float colonizePRogressPercetage = 0;
    bool planetColonized = false;
    
    void Start()
    {
        SolarSystemManager.singleton.RegisterPlanet(this);
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;


    }

    

    public void SetColonizeProgress(float t){ // t should be from 0 to 1
        colonizeProgressTransform.localScale = Vector3.one * t;
    }

    // void OnTriggerStay2D(Collider2D other){ // will continue to give true as we are on the object per frame
    //     colonizePRogressPercetage += Time.fixedDeltaTime * colonizeSpeed;
    //     if(colonizePRogressPercetage > 1){
    //         colonizePRogressPercetage = 1;
    //     }
    //     SetColonizeProgress(colonizePRogressPercetage);
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ship")){
            spriteRenderer.color = visitColor;
            //Changes color of ship as it enters 
            //other.GetComponent<SpriteRenderer>().color = Color.black; 

            //make the ship smaller as it enters
            //other.transform.localScale = new Vector3(.5f, .5f, .5f);

            visitors += 1;
        }
    
    }

    void Update(){
        HandleColonization();
    }


    void HandleColonization(){
        if(planetColonized){
            return;
        }

        colonizePRogressPercetage += (Time.deltaTime / colonizeTime) * visitors;
        if(colonizePRogressPercetage > 1){
            planetColonized = true;
            SolarSystemManager.singleton.ReportPlanetColonization();
            colonizeProgressSpriteRenderer.color = colonizedColor;
            colonizePRogressPercetage = 1;
        }
        SetColonizeProgress(colonizePRogressPercetage);
    }


    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Ship")){
            visitors -= 1;
            if (visitors < 1){
                spriteRenderer.color = defaultColor;
                visitors = 0;
            }
            
            //Changes color of ship as it leaves
            //other.GetComponent<SpriteRenderer>().color = Color.white;

            //make the ship back to original size as it leaves
            //other.transform.localScale = Vector3.one;
        }
    }

}
