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
    
    // Update is called once per frame
    void Update()
    {
        if (effectActive)
        {
            playerRb.AddForce(GetDirection() * Time.deltaTime, ForceMode.Acceleration);
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

    private Vector3 GetDirection()
    {
        var vec = transform.position - player.gameObject.transform.position;
        return vec.normalized * gravity;
    }

    private void Initialize()
    {
        playerRb = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }
    
    public void SetPlayer(GameObject gameObject)
    {
        player = gameObject;
        Initialize();
    }

    public void ShotThis(Vector3 targetPosition , float speed, float weight)
    {
        targetPos = targetPosition;
        originPos = transform.position;
        gravity = weight;
        
        effectActive = false;
        rb.velocity = (targetPos - originPos).normalized * speed;
    }

    private bool CheckArrival()
    {
        return (targetPos - originPos).magnitude <= (transform.position - originPos).magnitude + 0.1;
    }
    
}
