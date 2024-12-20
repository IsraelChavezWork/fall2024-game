using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemGenerator : MonoBehaviour
{

    [SerializeField] GameObject sunPrefab;
    [SerializeField] GameObject orbitPlanetPrefab;
    [SerializeField] GameObject asteroidPrefab;
    // Start is called before the first frame update
    public void Start(){
        Generate(0);
    }

    public void Generate(int seed){
        Random.InitState(seed);

        //sun
        GameObject newSun = Instantiate(sunPrefab, Vector3.zero, Quaternion.identity);
        newSun.transform.localScale = Vector3.one * Random.Range(3f,5f);
        newSun.GetComponent<SpriteRenderer>().color = new Color(1f, Random.Range(0f,1f), Random.Range(0f, 0.25f));

        //planets
        int numPlanets = Random.Range(1, 9);
        float planetDistanceTracker = Random.Range(3f,4f);
        
        int orbitDirection = Random.Range(0,3);
        
        if(orbitDirection == 2){
            orbitDirection = -1;
        }

        for (int i=0; i<numPlanets; i++){
            OrbitPlanet newPlanet = Instantiate(orbitPlanetPrefab, newSun.transform.position, Quaternion.identity).GetComponent<OrbitPlanet>();
            newPlanet.SetFollow(newSun.transform);
            newPlanet.SetOrbitDistance(planetDistanceTracker);
            planetDistanceTracker += Random.Range(3f, 6f);
            newPlanet.SetScale(Random.Range(1f, 2.5f));
            newPlanet.SetOrbitSpeed(Random.Range(10f,15f) * orbitDirection);
            newPlanet.SetRandomRotation(); 
            newPlanet.RandomizeColor();

            if(Random.Range(0,2) == 0){ //50%
                OrbitPlanet newMoon = Instantiate(orbitPlanetPrefab, newPlanet.transform.position, Quaternion.identity).GetComponent<OrbitPlanet>();
                newMoon.SetFollow(newPlanet.GetPlanetProper());
                newMoon.SetOrbitDistance(Random.Range(2.5f, 3f));
                newMoon.SetScale(Random.Range(0.5f, 0.75f));
                newMoon.SetOrbitSpeed(Random.Range(30f, 60f));
                newMoon.SetRandomRotation();
                newMoon.RandomizeColor();
            }
        }

        //Asteroids
        float asteroidBeltDistance = Random.Range(6f,30f);

        for(int i=0; i<60; i++){
            OrbitPlanet newAsteroid = Instantiate(asteroidPrefab, newSun.transform.position, Quaternion.identity).GetComponent<OrbitPlanet>();
            newAsteroid.SetOrbitDistance(asteroidBeltDistance + Random.Range(-2f,2f));
            newAsteroid.SetOrbitSpeed(10f);
            newAsteroid.SetFollow(newSun.transform);
            newAsteroid.SetRandomRotation();
            newAsteroid.SetScale(Random.Range(1f,4f));
        }
        
    }
}
