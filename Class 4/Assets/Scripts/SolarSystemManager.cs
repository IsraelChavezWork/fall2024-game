using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolarSystemManager : MonoBehaviour
{

    public static SolarSystemManager singleton; // good for sharing data between multiple instances of the same type

    [Header("UI")]
    [SerializeField] NoticeText jumpText;
    
    [Header("Planets")]
    [SerializeField] List<Planet> planets;
    [SerializeField] int planetsColonized = 0;
    [SerializeField] ScreenFader hyperJumpScreenFader;
    bool colonizedEntireSystem = false;

    void Awake()
    {
        // if there are no init instance that has take the og role, the 1st instace will take the og status for the main reference
        if(singleton == null){
            singleton = this;
        }else{
            //Debug.Log("Multiple solar system managers in the scene :c");
            Destroy(this.gameObject); //meaning, if someone already is a solar system manager, the duped one will be deleted
        }
    }

    public void RegisterPlanet(Planet p){
        planets.Add(p);
    }

    public void ReportPlanetColonization(){
        planetsColonized +=1;
        if(planetsColonized == planets.Count){
            //Debug.Log("VICTORY");
            colonizedEntireSystem = true;
            jumpText.ShowText();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //We could also do somehting like this to check if we won the game
        // for(int i=0; i<planets.Count; i++){ 
        //     if(planet.notColonized()){
        //         winGame = false;
        //     }
        // }
        // if(winGame){
        //     //do something
        // }
    }

    public void JumpAwayFromSystem(){
        if(!colonizedEntireSystem){
            return;
        }
        //hyperJumpScreenFader.FadeToColor();
        hyperJumpScreenFader.FadeInAndMove();
        StartCoroutine(DelayLeaveLevelAfterJump());
    }

    IEnumerator DelayLeaveLevelAfterJump(){
        //The code below does the same function as the whole weird new waint until thing bellow it :O
        // while(!hyperJumpScreenFader.DoneFadingToColor()){
        //     yield return null;
        // }

        yield return new WaitUntil(()=>hyperJumpScreenFader.DoneFadingToColor());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
