using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] SpaceShip playerShip;
   
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if(playerShip.IsDead()){
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.Space)) {
            playerShip.LaunchWithShip();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            playerShip.GetProjectileLauncher().Reload();
        }

        if (Input.GetKeyDown(KeyCode.J)){
           SolarSystemManager.singleton.JumpAwayFromSystem();
        }

        playerShip.AimShip(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            movement += new Vector3(0, 1, 0);

        }

        if (Input.GetKey(KeyCode.S)) {
            movement += new Vector3(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.A)) {
            movement += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            movement += new Vector3(1, 0, 0);
        }

        
        
        playerShip.Move(movement);
        // playerShip.MoveToward(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //travel towards mouse lol
    }

    public SpaceShip GetPlayerShip(){
        return playerShip;
    }

}
