using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoticeText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;

    public void ShowText(){
        text.gameObject.SetActive(true);
    }

    public void HideText(){
        text.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        HideText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
