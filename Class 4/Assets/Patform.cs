using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Patform : MonoBehaviour
{
    [SerializeField] Transform platform;
    
    [Header("Movement Of Platform")]
    [SerializeField] int timeToCompleteRoute = 3;
    [SerializeField] int speed = 10;
    [SerializeField] float pause = 1.5f;

    [Header("Position Of Platform")]
    //[SerializeField] float startPositionX = 0.0f;
    [SerializeField] float goalPositionX = 5.0f;
    //[SerializeField] float startPositionY = 0.0f;
    [SerializeField] float goalPositionY = 5.0f;

    Vector2 startPosition;
    Vector2 goalPosition;
    Vector2 targetPostion;
    bool isPause = false;

    void Start()
    {
        startPosition = new Vector2 (platform.position.x, platform.position.y);
        goalPosition = new Vector2 (goalPositionX, goalPositionY);
        targetPostion = goalPosition;
    }

    void Update()
    {
        if (isPause){
            return;
        }

        float moveSpeed = speed * Time.deltaTime * (1f / timeToCompleteRoute);
        platform.position = Vector2.MoveTowards(platform.position, targetPostion, moveSpeed);

        if ((Vector2)platform.position == targetPostion){
            StartCoroutine(PauseAtPoint());

        }
    }

    System.Collections.IEnumerator PauseAtPoint(){
        isPause = true;
        yield return new WaitForSeconds(pause);

        // weird logic that i had to look up 
        targetPostion = targetPostion == goalPosition ? startPosition : goalPosition;
        isPause = false;
    }
}
