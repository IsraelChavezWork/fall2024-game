using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;
    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;

    [Header("Tools")]
    [SerializeField] ProjectileLauncher projectileLauncher;

    bool dead = false;
    //[SerializeField] ProjectileLauncher rightProjectileLauncher;

    //public Transform planet;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //rotate 45 degrees counter-clock wise
        //transform.eulerAngles = new Vector3(0, 0, 45);
    }

    // Update is called once per frame
    void Update()
    {
        //AimShip(planet);
    }

    //Vector.forward == new Vector3(0,0,1)
    public void AimShip(Transform targetTransform){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimShip(targetTransform.position);
    }

    public void AimShip(Vector3 aimPos){
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);  //very snappy and insta rotation
        //Vector3.Lerp(pos1,pos2, 0.5f); how to do it in vector
        //Color.Lerp(Color.black, Color.white, 1f); //will give a color between these two colors, mkaing a grey color

        //gradual rotation to where we are aiming: v
        Quaternion goalRotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
        Quaternion currentRotation = transform.rotation;

        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);  //this one is more at point and snapyy
        //transform.rotation = Quaternion.Slerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);  //this one is quikcer, but slows down when close to where we aim, good for shooter
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > speedLimit){
            rb.velocity = rb.velocity.normalized * speedLimit; //lock our speed limit, in a cool way tho :O
        }
    }

    public void Move(Vector3 movement){

        //this movement is kinda of like glading on ice
        rb.AddForce(movement * speed); 
    }

    public void recoil(Vector3 amount){
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void LaunchWithShip(){
        recoil(-transform.up * projectileLauncher.Launch());
        //rightProjectileLauncher.Launch();
    }

    public void MoveToward(Vector3 goalPos){
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized);
    }

    public ProjectileLauncher GetProjectileLauncher(){
        return projectileLauncher;
    }

    public void Damage(){
        dead = true;
    }

    public bool IsDead(){
        return dead;
    }
}
