using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexManager : MonoBehaviour
{


    [SerializeField] private GameObject _prefab, player;

    [SerializeField] private GameObject[] pool;
    
    [SerializeField] private int activeLimit = 3;
    [SerializeField] private int poolLimit = 5;
    private int endNum = 0, activeNum = 0;
    private Camera _camera;
    
    
    static public VortexManager Instance;
    

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialized();
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void ShotVortex()
    {
        
    }
    
    private void Initialized()
    {
        _camera = Camera.main;
        pool = new GameObject[poolLimit];
        
        GameObject obj;

        for (var i = 0; i < poolLimit; i++)
        {
            obj = GameObject.Instantiate(_prefab);
            obj.SetActive(false);
            pool[i] = obj;
        }
    }
    
    public void MakeVortex(Vector3 argPos)
    {
        activeNum++;
        if (activeLimit < activeNum)
        {
            var pas = endNum - activeLimit;
            if (pas < 0)
            {
                pas = poolLimit + pas;
            }
            
            SetNonActive(pas);
        }
        
        pool[endNum].transform.position = argPos;
        pool[endNum].GetComponent<Vortex>().SetUp(player);
        pool[endNum].SetActive(true);
        endNum++;
        if (poolLimit <= endNum)
        {
            endNum = 0;
        }




    }


    public void SetNonActive(int pas)
    {
        pool[pas].SetActive(false);
    }
}
