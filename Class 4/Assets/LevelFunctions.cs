using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFunctions : MonoBehaviour
{
    
    public void goToNextLevel(){
        string[] levelNames = {"Area1", "Area2", "Area3"};
        int index = PlayerPrefs.GetInt("Level", 0) + 1;
        if(index >= 3){  //meaning we are in the last level
            index = 0;
        }
        PlayerPrefs.SetInt("NextLevel", index);
        SceneManager.LoadScene(levelNames[index]);
    }
}
