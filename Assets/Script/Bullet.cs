using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float mathTime = 0f, lifeTime = 5f;
    
    private Rigidbody _rb;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= mathTime)
        {
            gameObject.SetActive(false);
        }

        mathTime += Time.deltaTime;
    }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    public void Shot(Vector3 dir, float speed)
    {
        _rb.velocity = dir * speed;
        mathTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IHitBullet>() != null)
        {
            //当たった処理
            
            gameObject.SetActive(false);
        }
    }
}
