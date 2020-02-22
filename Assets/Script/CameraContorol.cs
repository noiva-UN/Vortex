using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorol : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField] private float permissibleRange;

    [SerializeField] private GameObject playerObject;
    
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position - playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position + offset;
    }
}
