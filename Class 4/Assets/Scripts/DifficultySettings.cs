using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown difficultyDropwdown;
    // Start is called before the first frame update
    void Start()
    {
        difficultyDropwdown.value = PlayerPrefs.GetInt("DifficultyIndex", 0);
    }

    public void SetDifficulty(){
        int index = difficultyDropwdown.value;
        string difficulty = difficultyDropwdown.options[index].text;
        PlayerPrefs.SetInt("DifficultyIndex", index);
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
}
