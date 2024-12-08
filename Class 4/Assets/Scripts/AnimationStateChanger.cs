using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    // public enum AnimationState{Idle, Walk};
    // [SerializeField] AnimationState currentEnumState = AnimationState.Idle; // or AnimationState.Walk
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Idle";
    // Start is called before the first frame update

    void Start()
    {
        ChangeAnimationState("Idle");
    }
    public void ChangeAnimationState(string newState, float speed = 1){
        animator.speed = speed;

        if (currentState == newState){
            return;
        }
        
        currentState = newState;
        animator.Play(currentState);
    }
}
