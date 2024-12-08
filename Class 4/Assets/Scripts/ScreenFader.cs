using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    [SerializeField] Image fadeImage;
    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadeTime = 1;
    [SerializeField] bool fadeFromColorOnStart = false;
    bool fading = false;
    bool doneFadingToColor = false;

    void Start()
    {
        if(fadeFromColorOnStart){
            //FadeFromColor();
            FadeOutAndMove();
        }
        
    }
    public void FadeToColor(){ //clear to opaque

        if(fading){
            return;
        }

        fading = true;
        StartCoroutine(FadeToColorRoutine());
            IEnumerator FadeToColorRoutine(){
                float t=0;
                while(t<fadeTime){
                    yield return null;
                    t+=Time.deltaTime;
                    fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, t/fadeTime);
                }

                fadeImage.color = fadeColor; //Why we do this? to make tsure we allway get the right color 100% of the times
                fading = false;
                doneFadingToColor = true;
                yield return null;
            }
    }


    public bool DoneFadingToColor(){
        return doneFadingToColor;
    }


    public void FadeFromColor(){ //opaque to clear
        if(fading){
            return;
        }

        fading = true;
        fadeImage.color = fadeColor;
        StartCoroutine(FadeFromColorRoutine());
            IEnumerator FadeFromColorRoutine(){
                float t=0;
                while(t<fadeTime){
                    yield return null;
                    t+=Time.deltaTime;
                    fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f-(t/fadeTime));
                }

                fadeImage.color = Color.clear; //Why we do this? to make tsure we allway get the right color 100% of the times
                fading = false;
                yield return null;
            }
    }


    public void FadeInAndMove(){
        if (fading){
            return;
        }

        fading = true;
        StartCoroutine(FadeInAndMoveRoutine());

        IEnumerator FadeInAndMoveRoutine(){
            fading = true;
            float time = 0.0f;
            UnityEngine.Vector2 startPosition = new UnityEngine.Vector2(1900,0);
            UnityEngine.Vector2 EndPosition = new UnityEngine.Vector2(0,0);

            while(time<fadeTime){
                yield return null;
                time+=Time.deltaTime;
                fadeImage.rectTransform.anchoredPosition = UnityEngine.Vector2.Lerp(startPosition, EndPosition, time/fadeTime);
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, time/fadeTime);
                


            }

            fadeImage.color = fadeColor;
            fadeImage.rectTransform.anchoredPosition = EndPosition;
            fading = false;
            doneFadingToColor = true;
            yield return null;
        }
    }


    public void FadeOutAndMove(){
        if (fading){
            return;
        }

        fading = true;
        StartCoroutine(FadeOutAndMoveRoutine());

        IEnumerator FadeOutAndMoveRoutine(){
            fading = true;
            float time = 0.0f;
            UnityEngine.Vector2 startPosition = new UnityEngine.Vector2(0,0);
            UnityEngine.Vector2 EndPosition = new UnityEngine.Vector2(1900,0);

            while(time<fadeTime){
                yield return null;
                time+=Time.deltaTime;
                fadeImage.rectTransform.anchoredPosition = UnityEngine.Vector2.Lerp(startPosition, EndPosition, time/fadeTime);
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f-(time/fadeTime/10));
                

            }

            fadeImage.color = Color.clear;
            fadeImage.rectTransform.anchoredPosition = EndPosition;
            fading = false;
            yield return null;
        }
    }
    
    //left at -1900L 1900R
    //center at 0L 0L 0R
    //right at 1900L -1900R
}
