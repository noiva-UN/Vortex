using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{

    [SerializeField] private bool effectActive = false;

    [SerializeField] private GameObject player;

    private Rigidbody playerRb = null, rb = null;

    private float mathTime = 0f, gravity = 1f;

    private Vector3 targetPos = Vector3.one, originPos = Vector3.one;

    private int count = 0;
    private Animator _animator;
    private static readonly int CountInAnimator = Animator.StringToHash("Count");

    // Update is called once per frame
    void Update()
    {
        if (effectActive)
        {
            playerRb.AddForce(GetDirection() * Time.deltaTime * (count / 2), ForceMode.Acceleration);
        }
        else
        {
            if (targetPos == Vector3.one) return;

            if (CheckArrival())
            {
                effectActive = true;
                rb.velocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// プレイヤーから見た渦の方向の単位ベクトルを得る
    /// </summary>
    /// <returns>単位ベクトル</returns>
    private Vector3 GetDirection()
    {
        var vec = transform.position - player.gameObject.transform.position;
        return vec.normalized * gravity;
    }

    /// <summary>
    /// 初期設定、生成後に一回だけ呼ぶやつ
    /// </summary>
    public void SetUp(GameObject playerObject)
    {
        player = playerObject;
        playerRb = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 初期化　配置後に1回だけ呼ぶ奴
    /// </summary>
    private void Initialized()
    {
        count = 3;
        _animator.SetInteger(CountInAnimator,count);
    }

    
    /// <summary>
    /// 渦の現在位置から次の位置に向けて打ち出す
    /// </summary>
    /// <param name="targetPosition">目標</param>
    /// <param name="speed">スピード</param>
    /// <param name="weight">重力</param>
    public void ShotThis(Vector3 targetPosition , float speed, float weight)
    {
        gameObject.SetActive(true);
        targetPos = targetPosition;
        originPos = transform.position;
        gravity = weight;
        
        effectActive = false;
        rb.velocity = (targetPos - originPos).normalized * speed;
        Initialized();
    }

    /// <summary>
    /// 他の渦が発射されたときに呼ぶやつ
    /// </summary>
    public void ShotOther()
    {
        count -= 1;
        _animator.SetInteger(CountInAnimator, count);
    }
    
    /// <summary>
    /// 目的地に着いたかどうか
    /// </summary>
    /// <returns>着いた(true/false)</returns>
    private bool CheckArrival()
    {
        return (targetPos - originPos).magnitude <= (transform.position - originPos).magnitude + 0.1;
    }
    
}
