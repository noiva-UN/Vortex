using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlayToBase : Enemy
{
    [SerializeField] private float speed = 2;
    
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetUp(EnemySpone spone,Base @base)
    {
        base.SetUp(spone, @base);
        target = @base;
        dir = (target.transform.position - transform.position).normalized;
    }
    public override void Initialize()
    {
        base.Initialize();
        dir = (target.transform.position - transform.position).normalized;
        _rigidbody.AddForce(dir * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == target)
        {
            target.BeAttack(pow);
        }
    }
}
