using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform _spher;
    private Vector3 _offSet;
    private void Start()
    {
        _spher = Player.PlayerMain.transform;
        _offSet = transform.position - _spher.position;
    }
    void FixedUpdate()
    {
        //if (StaticManager.IsStartGame)
            transform.position = _spher.position + _offSet;
    }
}
