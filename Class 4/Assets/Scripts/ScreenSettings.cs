using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropwdown;
    [SerializeField] Toggle VSync;
    [SerializeField] Toggle FullScreen;


    Resolution[] resolutions;

    void Start()
    {
        VSync.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("vSyncIsOn",0));
        FullScreen.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenIsOn",0));
        
        resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length -1); 

        for(int i=0; i<resolutions.Length; i++){
            string resolutionString = resolutions[i].width + "x" + resolutions[i].height.ToSafeString();
            resolutionDropwdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
            
        }
        currentResolutionIndex = Math.Min(currentResolutionIndex, resolutions.Length-1);
        resolutionDropwdown.value = currentResolutionIndex;
        SetResolution();

    }

    public void SetResolution()
    {
        int rezIndex = resolutionDropwdown.value;
        Screen.SetResolution(resolutions[rezIndex].width, resolutions[rezIndex].height, true);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropwdown.value); 
    }

    public void ActWithToggleVSync(){
        if (VSync.isOn){
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("vSyncIsOn", 1);
        }
        else{
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vSyncIsOn", 0);
        }
    }

    public void ActWithToggleFullScreen(){
        if(FullScreen.isOn){
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("FullScreenIsOn", 1);
        }
        else{
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("FullScreenIsOn", 0);
        }
    }
    
}
