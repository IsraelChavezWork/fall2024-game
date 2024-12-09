using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class PlayerInputHandlerFINAL : MonoBehaviour
{
    [SerializeField] PlayerTestCamera player;
    [Header("Map View Type")]
    [SerializeField] bool isSideView = false;
    
    /*
    isSideView = false == is a top down control type, moev all arround
    isSideView = true == is a side view, meaning that only jump and side movement
    */
   
    void Start()
    {
        
        bool hasBuyStamina = Convert.ToBoolean(PlayerPrefs.GetInt("hasBuyStamina", 0));
        if(hasBuyStamina){
            player.addMaxLung(50);
        }

        bool hasBuyItFlash = Convert.ToBoolean(PlayerPrefs.GetInt("hasBuyFlash", 0));
        if(hasBuyItFlash){
            Light2D surroundLight = player.getSurrounding();
            surroundLight.intensity = 0.21f;
            surroundLight.pointLightInnerRadius =0;
            surroundLight.pointLightOuterRadius = 1.73f;
            surroundLight.pointLightInnerAngle =0;
            surroundLight.pointLightOuterAngle = 277.71f;

            Light2D flashLight = player.getFlashlight();
            flashLight.intensity = 1f;
            flashLight.pointLightInnerRadius =0.84f;
            flashLight.pointLightOuterRadius = 5.19f;
            flashLight.pointLightInnerAngle =52.3f;
            flashLight.pointLightOuterAngle = 150f;


        }

        bool hasBuyItJump = Convert.ToBoolean(PlayerPrefs.GetInt("hasBuyJump", 0));
        if(hasBuyItJump){
            player.addMaxJump(1);
        }

        bool hasBuyItRecovery = Convert.ToBoolean(PlayerPrefs.GetInt("hasBuyRecovery", 0));
        if(hasBuyItRecovery){
            player.addRecoveryEff(0.2f);
        }
    }
    bool weWantToJump= false;

    void Update()
    {
        weWantToJump= false;
        

        if(Input.GetKeyDown(KeyCode.Space)){
            if(isSideView){
                if (player.getJumps() > 0){
                    player.recoverJump(true); //we restart the timer for stamina gain
                    int jumpsRemain = player.getJumps()-1;
                    player.setJumps(jumpsRemain);             
                    weWantToJump = true;
                   
                }
                else{
                    player.recoverJump(true); //we restart the timer for stamina gain
                }
                
                
            }
        }
        // playerShip.MoveToward(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //travel towards mouse lol
    }


    void FixedUpdate()
    {
        if(!isSideView){
            player.AimAtMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        float SprintMult = 1f;  //if spriting, increase the movement by ser.Field
        Vector3 movement = Vector3.zero;
        player.recoverStamina();
        player.recoverJump();

        if( Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            if (player.getLungCapacity()<0){
                player.recoverStamina(true); //we restart the timer for stamina gain
            }
            else{
            player.recoverStamina(true); //we restart the timer for stamina gain
            
            SprintMult = player.getSprintfMult(); //acutally increse the mult to 40% speed
            float lungCapacity = player.getLungCapacity() - player.getSprintDrain();
            player.setLungCapacity(lungCapacity);
            }

            /*if (player.getLungCapacity()<0 ){
                SprintMult = 1f; //in case we dont have stamina, walk nornally 
            }*/
        }

        if (Input.GetKey(KeyCode.W)) {
            if(!isSideView){
                movement += new Vector3(0, 1*SprintMult, 0);
            }
            

        }

        if (Input.GetKey(KeyCode.S)) {
            if(!isSideView){
            movement += new Vector3(0, -1*SprintMult, 0);
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            movement += new Vector3(-1*SprintMult, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            movement += new Vector3(1*SprintMult, 0, 0);
        }

        if(weWantToJump){
            movement += new Vector3(0,player.getJumpForce(),0); 
        }

        player.Walk(movement);
        // playerShip.MoveToward(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //travel towards mouse lol
    }

    public PlayerTestCamera GetPlayer(){
        return player;
    }

}
