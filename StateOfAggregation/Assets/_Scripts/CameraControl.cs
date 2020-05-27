using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform _spher;
    [SerializeField]
    private Vector3 _targetPos;
    void FixedUpdate()
    {
        if (StaticManager.IsStartGame)
            transform.position = _spher.position - _targetPos;
    }
}
