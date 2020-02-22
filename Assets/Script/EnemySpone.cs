using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpone : MonoBehaviour
{
    
    private List<Enemy> _enemyPool = new List<Enemy>();

    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int awakeSpone = 5, sponeLimit = 10;
    [SerializeField] private float sponeCoolDown = 1;
    
    private int activeEnemy = 0;
    private float mathTime = 0f;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeEnemy < sponeLimit)
        {
            if (mathTime >= sponeCoolDown)
            {
                Spone();
                mathTime = -Time.deltaTime;
            }

            mathTime += Time.deltaTime;
        }
        
    }

    private void Initialize()
    {
        for (var i = 0; i < awakeSpone; i++)
        {
           InitializedEnemy(NewEnemy());
        }
    }

    private void Spone()
    {

        foreach (var inpool in _enemyPool)
        {
            if (inpool.gameObject.active == false)
            {
                InitializedEnemy(inpool);
                return;
            }
        }
        InitializedEnemy(NewEnemy());

    }
    
    private Vector3 RandomPos()
    {
        var vec = new Vector3(Random.Range(-19, 20), Random.Range(-19, 20));
        foreach (var ene in _enemyPool)
        {
            if (Mathf.Abs((ene.gameObject.transform.position - vec).magnitude) <= 0.01f)
            {
                vec = RandomPos();
            }
        }
        return vec;
    }

    private Enemy NewEnemy()
    {
        var spone = Instantiate(enemyObject).GetComponent<Enemy>();
        spone.transform.SetParent(transform);
        spone.SetUp(this);
        _enemyPool.Add(spone);
        return spone;
    }

    private void InitializedEnemy(Enemy enemy)
    {
        enemy.transform.position = RandomPos();
        enemy.Initialize();
        activeEnemy++;
    }

    public void Dead()
    {
        activeEnemy -= 1;
    }
}
public interface IEnemy
{
    bool Active { get; set; }
    
    Vector3 GetPos();
    
    void BeShot();

    void Initialize();
}
