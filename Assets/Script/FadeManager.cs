using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    private static float alpha = 0.0f;

    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    private static float fadeTime = 0.2f;

    private static int nextScene = 1;


    static void Init()
    {
        var fadeObject = new GameObject("CanvasFade");
        fadeCanvas = fadeObject.AddComponent<Canvas>();
        fadeObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeObject.AddComponent<FadeManager>();
        DontDestroyOnLoad(fadeObject);
        
        fadeCanvas.sortingOrder = 100;

        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;
        DontDestroyOnLoad(fadeImage);
        
        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    public static void FadeIn()
    {
        if (fadeImage == null) Init();
        fadeImage.color=Color.clear;
        isFadeIn = true;
    }

    public static void FadeOut(int n)
    {
        if(fadeImage==null) Init();
        nextScene = n;
        fadeImage.color = Color.clear;
        isFadeOut = true;
    }

    void Update()
    {
        if (isFadeIn)
        {
            alpha -= Time.deltaTime / fadeTime;

            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }else if (isFadeOut)
        {
            alpha += Time.deltaTime / fadeTime;

            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                SceneManager.LoadScene(nextScene);
            }
            
            fadeImage.color=new Color(0.0f,0.0f,0.0f,alpha);
        }
    }
}
