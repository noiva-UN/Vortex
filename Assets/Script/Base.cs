using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : SingletonMonoBehaviour<Base>
{
    [SerializeField] private int Maxhp;
    private int hp;

    // Start is called before the first frame update
    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeAttack(int pow)
    {
        hp -= pow;
    }
}
