using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScenesManager : MonoBehaviour
{
    [SerializeField] private Text message,title;

    [SerializeField] private GameObject buttons;

    [SerializeField] private float titleMoveSpeed = 1f;
    
    private int phase = 0;
    
    void Awake()
    {
        FadeManager.FadeIn();
        Init();
        
    }

    void Update()
    {
        if (Input.anyKey && phase==0)
        {
            phase++;
            StartCoroutine(MoveTitle());
        }
    }

    private void Init()
    {
        buttons.SetActive(false);
        title.rectTransform.anchoredPosition=Vector3.zero;
        message.text = "Push any key or button";
        phase = 0;
    }

    IEnumerator MoveTitle()
    {
        while (true)
        {
            var now = title.rectTransform.anchoredPosition;

            now.y += titleMoveSpeed * Time.deltaTime;
            title.rectTransform.anchoredPosition = now;

            if (now.y >= 120)
            {
                title.rectTransform.anchoredPosition = new Vector3(0f, 120f, 0f);
                buttons.SetActive(true);
                break;
            }

            yield return null;
        }
    }

    public void ButtonAct(int n)
    {
        FadeManager.FadeOut(n);
    }
}
