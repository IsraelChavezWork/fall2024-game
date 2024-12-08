using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerTestCamera : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;

    [Header("Max Stats")]
    [SerializeField] int maxLifes = 3;
    [SerializeField] float maxLungCapacity = 100.0f;
    [SerializeField] int maxJumps = 1;

    [Header("Player Wearable")]
    [SerializeField] int lifes = 2;
    [SerializeField] float lungCapacity = 100.0f;
    [SerializeField] int jumps = 1;
    
    [Header("Player Stats")]
    [SerializeField] float timeToStartRecoveringSprint = 2.0f;
    [SerializeField] float recoverEfficiency = 0.1f;
    [SerializeField] float timerSprint = 0.0f;
    [SerializeField] float sprintMult = 1.3f;
    [SerializeField] float sprintDarin = 0.3f;
    

    [Header("Jumping Player Stats")]
    [SerializeField] int jumpForce = 30;
    [SerializeField] float timerJump = 0.0f;
    [SerializeField] float timeToStartRecoveringJump = 2.0f;


    [Header("Coins Collect Sounds")]
    [SerializeField] AudioClip coinPickup;
    [SerializeField] AudioSource coinAudioSource;
    
    [Range(0,1)]
    [SerializeField] float coinPitchRange = 0.2f; 
    [SerializeField] AudioMixerGroup coinAudioMixerGroup;

    [Header("Coins Collected")]
    [SerializeField] int purpleCoins = 0;
    [SerializeField] int greenCoins = 0;

     [Header("Heal Collect Sounds")]
    [SerializeField] AudioClip healPickup;
    [SerializeField] AudioSource healAudioSource;
    
    [Range(0,1)]
    [SerializeField] float medkitPitchRange = 0.2f; 
    [SerializeField] AudioMixerGroup medkitAudioMixerGroup;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        greenCoins = PlayerPrefs.GetInt("GreenCoins", 0);
        purpleCoins = PlayerPrefs.GetInt("PurpleCoins", 0);

        //purpleCoins = recoverPurpleCoins;
        //greenCoins = recoverGreenCoins;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AimAtMouse(Transform targetTransform){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimAtMouse(targetTransform.position);
    }

    public void AimAtMouse(Vector3 aimPos){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);  //very snappy and insta rotation
        //Vector3.Lerp(pos1,pos2, 0.5f); how to do it in vector
        //Color.Lerp(Color.black, Color.white, 1f); //will give a color between these two colors, mkaing a grey color

        //gradual rotation to where we are aiming: v
        Quaternion goalRotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
        Quaternion currentRotation = transform.rotation;

        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);  //this one is more at point and snapyy
        //transform.rotation = Quaternion.Slerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);  //this one is quikcer, but slows down when close to where we aim, good for shooter
    }

    public void MoveLikeGlading(Vector3 movement){

        //this movement is kinda of like glading on ice
        rb.AddForce(movement * speed); 
    }

    public void Walk(Vector3 movement){
        rb.velocity = movement * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GreenCoin")){
            coinPickupSound();
            greenCoins += 1;
            PlayerPrefs.SetInt("GreenCoins", greenCoins);
        }

        if(other.CompareTag("PurpleCoin")){
            coinPickupSound();
            purpleCoins += 1;
            PlayerPrefs.SetInt("PurpleCoins", purpleCoins);
        }

        if(other.CompareTag("MedKit")){
            medkitPickupSound();
            heal(1);
        }

        if(other.CompareTag("KillZone")){
            //do something
        }
    }

    public int getLifes(){
        return lifes;
    }

    public float getLungCapacity(){
        return lungCapacity;
    }

    public void setLifes(int life){
        if (!(lifes+life > maxLifes)){
        lifes = life;
        }
    }

    public void setLungCapacity(float capacity){
        if(capacity > maxLungCapacity){
            lungCapacity = maxLungCapacity;
        }
        lungCapacity = capacity;
    }

    public void restartSprintTimer(){
        timerSprint = 0.0f;
    }

    public void recoverStamina(bool doWeRestart = false){
        if(doWeRestart){
            restartSprintTimer();
        }
        timerSprint += Time.deltaTime;
        if (timerSprint >= timeToStartRecoveringSprint && lungCapacity<maxLungCapacity)
            lungCapacity += recoverEfficiency;
    } 

    public void restartJumpTimer(){
        timerJump = 0.0f;
    }

    public void recoverJump(bool doWeRestart = false){
        if(doWeRestart){
            restartJumpTimer();
        }
        else
        {    timerJump += Time.deltaTime;
            if (timerJump >= timeToStartRecoveringJump && jumps<maxJumps){
                jumps = maxJumps;
            }
        }
    } 

    public float getSprintDrain(){
        return sprintDarin;
    }

    public float getSprintfMult(){
        return sprintMult;
    }

    public int getJumpForce(){
        return jumpForce;
    }

    public int getJumps(){
        return jumps;
    }

    public void setJumps(int NumJumps){

        if(NumJumps < 0){
            jumps = 0;
        }
        else if(NumJumps > maxJumps){
            jumps = maxJumps;
        }else{
            jumps = NumJumps;
        }
        
    }

    void heal(int healAmount){
        if ((healAmount+1 <= maxLifes)){
            lifes += healAmount;
        }
    }
    
    void coinPickupSound(){
        coinAudioSource.clip = coinPickup;
        coinAudioSource.outputAudioMixerGroup = coinAudioMixerGroup;
        coinAudioSource.pitch = Random.Range(1f-coinPitchRange, 1f+coinPitchRange);
        coinAudioSource.Play();
    }

    void medkitPickupSound(){
        healAudioSource.clip = healPickup;
        healAudioSource.outputAudioMixerGroup = medkitAudioMixerGroup;
        healAudioSource.pitch = Random.Range(1f-medkitPitchRange, 1f+medkitPitchRange);
        healAudioSource.Play();
    }

    public int[] getCoins(){
        int[] arr = {greenCoins, purpleCoins};
        return arr;
    }

    public int getMaxLifes(){
        return maxLifes;
    }

}
