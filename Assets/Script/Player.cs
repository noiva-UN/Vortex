using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int Maxhp = 10, pow =1;
    [SerializeField] private float bulletSpeed = 5f, gravityPow = 2f;
    [SerializeField] private GameObject bullet, vortex;
    
    private Vortex[] _vortexPool;
    private List<Bullet> _bulletPool = new List<Bullet>();
    
    [SerializeField] private int poolLimit = 3;
    private int _vortexEndNum = 0;



    private Rigidbody _rb;
    private Camera _camera;
    
    // Start is called before the first frame update
    void Awake()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        MainCharaMove();
    }

    private void Initialized()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _vortexPool = new Vortex[poolLimit];

        for (var i = 0; i < poolLimit; i++)
        {
            var vor = Instantiate(vortex).GetComponent<Vortex>();
            
            vor.SetUp(gameObject);
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
    
    /// <summary>
    /// プレイヤーの操作面
    /// </summary>
    private void PlayerMove()
    {
        if (Input.GetMouseButtonDown(1))
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
            
        }else if (Input.GetMouseButtonDown(0))
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

    /// <summary>
    /// キャラクターの制御面
    /// </summary>
    private void MainCharaMove()
    {
        var axis = Vector3.Cross(Vector3.up, _rb.velocity.normalized);

        var angle = Vector3.Angle(Vector3.up, _rb.velocity.normalized) * (axis.z < 0 ? -1 : 1);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    /// <summary>
    /// 今渦がある位置から次の位置に移動する間に障害物があるか
    /// </summary>
    /// <param name="nextPos">次の位置</param>
    /// <returns></returns>
    private bool CheckCanMake(Vector3 nextPos)
    {
        var position = _vortexPool[_vortexEndNum].transform.position;
        var hit = Physics.RaycastAll(position, nextPos - position,
            (nextPos - position).magnitude);

        foreach (var obj in hit)
        {
            if (obj.transform.CompareTag("Wall"))
            {
                return false;
            }
        }

        return true;

    }
    
    /// <summary>
    /// 渦を移動させるメソッド等を呼んで、ポインタを動かす
    /// </summary>
    /// <param name="argPos">渦の移動先</param>
    public void MakeVortex(Vector3 argPos)
    {
        var vor=_vortexPool[_vortexEndNum];
        vor.ShotThis(argPos, bulletSpeed, gravityPow);

        for (var i = 0; i < _vortexPool.Length; i++)
        {
            if (i != _vortexEndNum)
                _vortexPool[i].ShotOther();
        }

        _vortexEndNum++;
        if (poolLimit <= _vortexEndNum)
        {
            _vortexEndNum = 0;
        }
    }

    
    /// <summary>
    /// バレットプール内で使われていないバレットを探す。無ければ新しく作る。
    /// </summary>
    /// <returns>使えるバレット</returns>
    public Bullet ActiveBullet()
    {
        foreach (var bullet in _bulletPool)
        {
            if (!bullet.gameObject.active)
            {
                bullet.gameObject.SetActive(true);
                return bullet;

            }
        }

        return ActiveNewBullet();
    }
    
    /// <summary>
    /// 新しくバレットプールにバレットを作る
    /// </summary>
    /// <returns>新しく作ったバレット</returns>
    public Bullet ActiveNewBullet()
    {
        var bul = Instantiate(bullet).GetComponent<Bullet>();
        bul.Initialize();
        _bulletPool.Add(bul);
        return bul;
    }

    public float Angle(Vector3 origin, Vector3 target)
    {
        var diff = origin - target;

        var axis = Vector3.Cross(Vector3.up, diff);

        var angle = Vector3.Angle(Vector3.up, diff) * (axis.z < 0 ? -1 : 1);

        return angle;
    }


    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.BeShot(pow);
        }
    }
}