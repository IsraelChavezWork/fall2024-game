using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerTestCamera : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;

    [Header("Player itself")]
    [SerializeField] GameObject playerHimSelf;

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

    [Header("Pain Sounds")]
    [SerializeField] AudioClip painArea;
    [SerializeField] AudioSource painAudioSource;
    
    [Range(0,1)]
    [SerializeField] float painPitchRange = 0.2f; 
    [SerializeField] AudioMixerGroup painAudioMixerGroup;

    [Header("Flashlight")]
    [SerializeField] Light2D Flashligth;
    [SerializeField] Light2D Surrounding;

    [Header("respawn Coordenates")]
    [SerializeField] float xCords;
    [SerializeField] float yCords;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        greenCoins = PlayerPrefs.GetInt("GreenCoins", 0);
        purpleCoins = PlayerPrefs.GetInt("PurpleCoins", 0);

        //lifes = maxLifes;
        //lungCapacity = maxLungCapacity;
        //jumps = maxJumps;

        //purpleCoins = recoverPurpleCoins;
        //greenCoins = recoverGreenCoins;

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
            if(getLifes() + 1 <= maxLifes){
                heal(1);
            }
        }

        if(other.CompareTag("Door")){
            
            string[] levelNames = {"Area1", "Area2", "Area3"};
            int index = PlayerPrefs.GetInt("Level", 0) + 1;
            if(index >= 3){  //meaning we are in the last level
                index = 0;
            }
        PlayerPrefs.SetInt("Level", index);
        SceneManager.LoadScene(levelNames[index]);
    
        }

        if(other.CompareTag("KillZone")){
            //make some ouch noice
            painSound();
            //if lifes -1 > 0 then go back to respanw
            if(lifes>1){
                //go back to respanw coords
                lifes -=1;
                Vector3 currPosition = transform.position;
                currPosition.x = xCords;
                currPosition.y = yCords;

                playerHimSelf.transform.position = currPosition;
            }else{
                //back to main menu
            }
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

    void painSound(){
        painAudioSource.clip = painArea;
        painAudioSource.outputAudioMixerGroup = painAudioMixerGroup;
        painAudioSource.pitch = Random.Range(1f-painPitchRange, 1f+painPitchRange);
        painAudioSource.Play();
    }

    public int[] getCoins(){
        int[] arr = {greenCoins, purpleCoins};
        return arr;
    }

    public int getMaxLifes(){
        return maxLifes;
    }

    public Light2D getFlashlight(){
        return Flashligth;
    }

    public Light2D getSurrounding(){
        return Surrounding;
    }

    public void addMaxLifes(int maxlifes){
        maxLifes += maxlifes;
    }

    public void addMaxLung(float lung){
        maxLungCapacity += lung;
    }

    public void addMaxJump(int jump){
        maxJumps += jump;
    }

    public void addRecoveryEff(float recovery){
        recoverEfficiency +=recovery;
    }

}
