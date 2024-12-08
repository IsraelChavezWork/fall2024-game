using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    [SerializeField] string currentStateString;

    [SerializeField] SpaceShip myShip;
    [SerializeField] SpaceShip targetShip;

    [Header("Config")]
    [SerializeField] float sightDistance = 10;

    delegate void AIState();
    AIState currentState;


    //trackers======================================

    float stateTime = 0;
    bool justChnagedState = false;
    Vector3 lastTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(IdleState);
    }

    void ChangeState(AIState newAIState){
        currentState = newAIState;
        justChnagedState = true;
    }

    bool CanSeeTarget(){
        return (Vector3.Distance(myShip.transform.position, targetShip.transform.position)) < sightDistance;
    }
    

    void IdleState(){
        if (stateTime == 0){ //if we just entred this state 
            currentStateString = "IdleState";
        }

        if (CanSeeTarget()){
            ChangeState(AttackState);
            return;
        }

    }

    void AttackState(){
        myShip.MoveToward(targetShip.transform.position);
        myShip.AimShip(targetShip.transform);

         if (stateTime == 0){ //if we just entred this state 
            currentStateString = "AttackState";
         }

        if(stateTime > 4){
            myShip.LaunchWithShip(); //shoot at player >:C
        }

        if(myShip.GetProjectileLauncher().GetAmmo() == 0){
            myShip.GetProjectileLauncher().Reload();
        }

        

        if (!CanSeeTarget()){
            lastTargetPos = targetShip.transform.position;
            ChangeState(GetBackToTargetState);
            return;
        }
    }

    void GetBackToTargetState(){ //if we lose sight of the player, go backk to the paostion wehere we last saw th eplayer

        if (stateTime == 0){ //if we just entred this state 
            currentStateString = "BackToTargetState";
        }

        myShip.MoveToward(lastTargetPos);
        myShip.AimShip(lastTargetPos);

        if(stateTime < 2){
            return;
        }

        if (CanSeeTarget()){
            ChangeState(AttackState);
            return;
        }

        if(Vector3.Distance(myShip.transform.position, lastTargetPos) < 1){
            ChangeState(PatrolState);
            return;

        }
    }

    Vector3 patrolPos;
    Vector3 patrolPivot;
    void PatrolState(){
        /*
        pick a random positon
        move towards it
        once we reach it, chosse a new random position
        */

        if (stateTime == 0){ //if we just entred this state 
            currentStateString = "PatrolState";
            patrolPivot = myShip.transform.position;
            patrolPos = myShip.transform.position + new Vector3(Random.Range(-sightDistance, sightDistance), Random.Range(-sightDistance, sightDistance)); // the reason why we used our postion + a vector is to get a close cordenate
        }

        myShip.MoveToward(patrolPos);
        myShip.AimShip(patrolPos);

        if(CanSeeTarget()){
            ChangeState(AttackState);
            return;
        }

        if(Vector3.Distance(myShip.transform.position, patrolPos) < 1f){
            patrolPos = patrolPivot + new Vector3(Random.Range(-sightDistance, sightDistance), Random.Range(-sightDistance, sightDistance));
            return;
        }

        


    }

    void AITick(){
        if(justChnagedState){
            stateTime = 0;
            justChnagedState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
        
    }

    void FixedUpdate()
    {
        //move the ship inside this method :c
        //set the movement install of calling move ship method
    }

    void Update()
    {
        AITick();
    }
}
