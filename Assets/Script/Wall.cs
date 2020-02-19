using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class Wall : MonoBehaviour,IHitBullet
{

    #region Interface

    public GameObject HitEffect { get; }

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

namespace Interface
{
    public interface IHitBullet
    {
        GameObject HitEffect { get; }
        
        
    }
}