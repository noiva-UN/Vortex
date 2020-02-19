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
    

    // Start is called before the first frame update
    private void Awake()
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
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30f))
            {
                var pos = hit.point;
                MakeVortex(pos);
            }
            
            

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
        pool[endNum].GetComponent<Vortex>().SetPlayer(player);
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
