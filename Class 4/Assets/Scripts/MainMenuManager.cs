using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Space");
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit(); //oonly work in the built game lol, not the editor
    }

    public void StartTutorial(){
        SceneManager.LoadScene("LightTesting");
    }

    public void StartStory(){
        SceneManager.LoadScene("LightTesting");
    }
}
