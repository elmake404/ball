using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform _spher;
    [SerializeField]
    private Vector3 _targetPos;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = _spher.position - _targetPos;
    }
}
