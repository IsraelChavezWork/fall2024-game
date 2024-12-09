using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateCoins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI greenCoinsText;
    [SerializeField] TextMeshProUGUI purpleCoinsText;

    // Start is called before the first frame update
    void Awake()
    {
        updateCoins();
    }

    // Update is called once per frame
    public void updateCoins(){
        int greenCoins = PlayerPrefs.GetInt("GreenCoins", 0);
        int purpleCoins = PlayerPrefs.GetInt("PurpleCoins", 0);

        greenCoinsText.text = greenCoins.ToString();
        purpleCoinsText.text = purpleCoins.ToString();
    }
}
