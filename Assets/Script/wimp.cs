using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wimp : Enemy
{

    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetUp(EnemySpone spone,Base @base)
    {
        base.SetUp(spone, @base);
        _audio = GetComponent<AudioSource>();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void Dead()
    {
        base.Dead();
        
        _audio.Play();

        StartCoroutine(Checking(_audio, () => { gameObject.SetActive(false); }));
    }
}


