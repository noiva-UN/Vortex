using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHp = 1, pow = 1;
    protected int hp;
    
    protected Rigidbody _rigidbody;

    protected EnemySpone _enemySpone;
    public virtual void SetUp(EnemySpone spone)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemySpone = spone;
        gameObject.SetActive(false);
    }
    
    public virtual void Initialize()
    {
        hp = maxHp;
        gameObject.SetActive(true);
    }

    public virtual void BeShot(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Dead();    
        }

    }

    protected virtual void Dead()
    {
        
    }

    protected delegate void FunctionType();

    protected IEnumerator Checking(AudioSource audio, FunctionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                _enemySpone.Dead();
                callback();
                break;
            }
        }
        
    }
}
