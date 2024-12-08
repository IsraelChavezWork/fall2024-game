using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 10;

    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Helpers")]
    [SerializeField] Transform spawnTransform;

    [Header("Audio")]
    //[SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    [Range(0,1)]
    [SerializeField] float pitchRange = .2f;

    [Header("Ammo")]
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentammo = 10;
    [SerializeField] float maxReloadTime = 3;
    [SerializeField] float cooldownTime = 0.25f;
    [SerializeField] float recoil = 2;
    [SerializeField] float projectileSize = 1f;
    [SerializeField] float accuarcy = 1;
    [SerializeField] int projectileCount = 1;



    float currentReloadTime = 0;

    [Header("Proc Gen")]

    [SerializeField] int seed = 0;

    int minGenAmmo = 1;
    int maxGenAmmo = 12;
    float minGenRealoadTime = 1;
    float maxGenReloadTime = 10;
    float minGenCooldownTime = 0.01f;
    float maxGenCooldownTime = 1f;
    float minGenRecoil = 0;
    float maxGenRecoil = 2;
    float minGenSize = 0.2f;
    float maxGenSize = 2f;
    float minGenProjectileSpeed = 2.5f;
    float maxGenProjectileSpeed = 10.0f;

    void Awake()
    {
        currentammo = maxAmmo;
        Generate(Random.Range(int.MinValue, int.MaxValue));
    }

    public void Generate(int newSeed){
        seed = newSeed;
        Random.InitState(newSeed);
        maxAmmo = Random.Range(minGenAmmo, maxGenAmmo+1);
        currentammo = 0; //it seems that when we spawn we cant shoot, but after realoding we go back to normality, wth?, let them just realod lmao, no idea why it dont shoot as soon as it spawns, >:c
        currentReloadTime = Random.Range(minGenRealoadTime, maxGenReloadTime);
        maxReloadTime = currentReloadTime;
        cooldownTime = Random.Range(minGenCooldownTime, maxGenCooldownTime);
        recoil = Random.Range(minGenRecoil, maxGenRecoil);
        projectileSpeed = Random.Range(minGenProjectileSpeed, maxGenProjectileSpeed);
        projectileSize = Random.Range(minGenSize, maxGenSize);
        accuarcy = Random.Range(0.5f,1f);
        projectileCount = Random.Range(1,10);

        // If we wanted to realod as soon as this woudl genearte, then we would need to include a serlized filed for the ship in question, and make it reload lol :c

    }

    bool coolingDown = false;

    void Start()
    {
        //Launch();
    }

    //Launch a projectile forward
    public float Launch(){
        // another way to get An audio soruce is by typing this: GetComponent<AudioSoruce>()
        if(currentammo < 1){
            return 0;
        }

        if(currentReloadTime > 0){
            return 0;
        }

        if (coolingDown){
            return 0;
        }
        Cooldown();

        currentammo -=1;
        for(int i=0; i<projectileCount; i++){
            GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, transform.rotation);
            newProjectile.transform.localScale = Vector3.one * projectileSize;
            newProjectile.transform.Rotate(new Vector3 (0, 0, Random.Range(-90+(90*accuarcy), 90-(90*accuarcy))));
            newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * projectileSpeed;
            Destroy(newProjectile, 20);
        }
        
        //newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        //audioSource.PlayOneShot(audioClip); //use audio sorice to play whaterver clip you pass WONT CUT AUDIO 

        audioSource.pitch = Random.Range(1f-pitchRange, 1f+pitchRange);
        audioSource.Play();
        //Destroy the projectile after 2 seconds
         

        return GetRecoilAmount();

        //Vector3.up == new Vector3(0,1,0)
    }

    void Cooldown(){
        coolingDown = true;
        StartCoroutine(CoolingDownRoutine());
        IEnumerator CoolingDownRoutine(){
            yield return new WaitForSeconds(cooldownTime);
            coolingDown = false;
        }
    }

    bool currentlyReloading = false;
    public void Reload(){

        if(currentlyReloading){
            return;
        }

        if(currentammo == maxAmmo){
            return;
        }

        currentlyReloading = true;
        currentReloadTime = 0;
        StartCoroutine(ReloadRoutine());

        IEnumerator ReloadRoutine(){
            Debug.Log("Reload routin active!");
            //yield return new WaitForSeconds(reloadTime); // just waiting

            while(currentReloadTime < maxReloadTime){
                yield return null; //this will make sure the couritne doesn't run forever, and also make sure to run this every frame :D
                currentReloadTime += Time.deltaTime;
            }
            currentReloadTime = 0;
            currentlyReloading = false;
            Debug.Log("Reload routin done!");
            currentammo = maxAmmo;
        }
    }

    public float GetReloadPercentage(){
        return currentReloadTime/maxReloadTime;
    }

    public float GetRecoilAmount(){
        return recoil;
    }

    public int GetAmmo(){
        return currentammo;
    }

    public int GetMaxAmmo(){
        return maxAmmo;
    }
}
