using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int Maxhp = 10;
    [SerializeField] private float bulletSpeed = 5f, gravityPow = 2f;
    [SerializeField] private GameObject bullet, vortex;
    
    private Vortex[] _vortexPool;
    private List<Bullet> _bulletPool = new List<Bullet>();
    
    [SerializeField] private int vortexActiveLimit = 3;
    [SerializeField] private int poolLimit = 5;
    private int _vortexEndNum = 0, _vortexActiveNum = 0;
    
    
    
    
    private Camera _camera;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        _vortexPool = new Vortex[poolLimit];

        for (var i = 0; i < poolLimit; i++)
        {
            var vor = Instantiate(vortex).GetComponent<Vortex>();
            
            vor.SetPlayer(gameObject);
            //vor.gameObject.SetActive(false);
            _vortexPool[i] = vor;
        }

        for (var i = 0; i < poolLimit; i++)
        {
            var bul = Instantiate(bullet).GetComponent<Bullet>();
            bul.Initialize();
            _bulletPool.Add(bul);
            bul.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        MainCharaMove();
    }

    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 30f))
            {
                var pos = hit.point;
                pos.z = 0f;

                var bullet = ActiveBullet();
                bullet.transform.position = transform.position;
                bullet.Shot((pos - transform.position).normalized, bulletSpeed);
            }
            
        }else if (Input.GetMouseButtonDown(1))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 30f))
            {
                var pos = hit.point;
                pos.z = 0f;
                
                if(!CheckCanMake(pos)) return;
                MakeVortex(pos);
            }
        }
    }

    private void MainCharaMove()
    {
        
    }


    private bool CheckCanMake(Vector3 argPos)
    {
        var position = transform.position;
        var hit = Physics.RaycastAll(position, argPos - position,
            (argPos - position).magnitude);

        foreach (var obj in hit)
        {
            if (obj.transform.CompareTag("Wall"))
            {
                return false;
            }
        }

        return true;

    }
    
    public void MakeVortex(Vector3 argPos)
    {
        _vortexActiveNum++;
        if (vortexActiveLimit < _vortexActiveNum)
        {
            var pas = _vortexEndNum - vortexActiveLimit;
            if (pas < 0)
            {
                pas = poolLimit + pas;
            }
            
            _vortexPool[pas].gameObject.SetActive(false);
        }

        _vortexPool[_vortexEndNum].transform.position = transform.position;
        _vortexPool[_vortexEndNum].ShotThis(argPos, bulletSpeed, gravityPow);
        _vortexPool[_vortexEndNum].gameObject.SetActive(true);
        _vortexEndNum++;
        if (poolLimit <= _vortexEndNum)
        {
            _vortexEndNum = 0;
        }
    }

    public Bullet ActiveBullet()
    {
        foreach (var bullet in _bulletPool)
        {
            if (!bullet.gameObject.active)
            {
                return ActiveInBulletPool(bullet);
                
            }
        }

        return ActiveNewBullet();
    }

    public Bullet ActiveInBulletPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    public Bullet ActiveNewBullet()
    {
        var bul = Instantiate(bullet).GetComponent<Bullet>();
        bul.Initialize();
        _bulletPool.Add(bul);
        return bul;
    }
}
