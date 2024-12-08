using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> asteroidPrefabs;
    [SerializeField] Transform playerTransform;
    // Start is called before the first frame update
    [SerializeField] float SpawnTime = 1f;
    [SerializeField] float spawnDistance = 100;
    [SerializeField] float startAsteroids = 10;
    [SerializeField] int maxAsteroids = 100;
    public float timer = 0;
    void Start()
    {
        SpawnStartingAsteroids();
        SpawnAsteroids();
    }

    void SpawnAsteroid(){
        Vector3 spawnPos = playerTransform.position + new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0).normalized * spawnDistance;
        Instantiate(asteroidPrefabs[Random.Range(0,asteroidPrefabs.Count)], spawnPos, Quaternion.identity);
    }

    // Update is called once per frame

    void SpawnAsteroidnearPlayer(){
        Vector3 spawnPos = playerTransform.position + new Vector3(Random.Range(-spawnDistance,spawnDistance), Random.Range(-spawnDistance,spawnDistance), 0).normalized * spawnDistance;
        Instantiate(asteroidPrefabs[Random.Range(0,asteroidPrefabs.Count)], spawnPos, Quaternion.identity);
    }

    void SpawnStartingAsteroids(){
        for(int i=0; i<startAsteroids ;i++){
            SpawnAsteroidnearPlayer();
        }
    }


    void SpawnAsteroids(){
        StartCoroutine(SpawnAsteroidsRoutine());
        IEnumerator SpawnAsteroidsRoutine(){
            int counter = 0;
            while(counter < maxAsteroids){
                yield return new WaitForSeconds(SpawnTime);
                SpawnAsteroid();
                counter ++;
            }

        //SpawnAsteroid();
        //yield return new WaitForSeconds(1);
        //yield return null // wwaits for a single frame
        //yield return new WaitForFixedUpdate(); // wait for a physics upate 1/50th second by default
        //SpawnAsteroid();
        }   
    }
    
    /*
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnTime){
            SpawnAsteroid();
            timer = 0;
        }
    }
    */
}
