using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform _spher;
    private Vector3 _offSet;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        _spher = Player.PlayerMain.transform;
        _offSet = transform.position - _spher.position;
    }
    void FixedUpdate()
    {
        if (StaticManager.IsStartGame)
            transform.position = Vector3.SmoothDamp(transform.position , _spher.position + _offSet,ref velocity,0.03f);
            //transform.position = _spher.position + _offSet;
    }
}
