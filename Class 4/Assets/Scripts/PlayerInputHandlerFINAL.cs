using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    }

    // Update is called once per frame
    /*void Update()
    {
        if(!isSideView){
            player.AimAtMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        
    }*/

    void /*Fixed*/Update()
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

        if(Input.GetKeyDown(KeyCode.Space)){
            if(isSideView){
                if (player.getJumps()>0){
                    player.recoverJump(true); //we restart the timer for stamina gain
                    
                    int jumpsRemain = player.getJumps()-1;
                    player.setJumps(jumpsRemain);
                    movement += new Vector3(0,player.getJumpForce(),0);
                   
                }
                else{
                     player.recoverJump(true); //we restart the timer for stamina gain
                }
                
                
            }
        }

        
        
        player.Walk(movement);
        // playerShip.MoveToward(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //travel towards mouse lol
    }

    public PlayerTestCamera GetPlayer(){
        return player;
    }

}
