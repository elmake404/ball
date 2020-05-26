using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    private Vector3 _startPos, _currentPos, _normPos;
    private Camera _cam;
    [SerializeField]
    private Material _water, _steam, _ice;
    [SerializeField]
    private MeshRenderer _mesh;

    [SerializeField]
    private float _speed, _speedSteem;
    void Start()
    {
        StaticManager.IsStartGame = true;
        _mesh.material = _water;
        _normPos = transform.position;
        _cam = Camera.main;
    }
    private void Update()
    {
        if (StaticManager.IsStartGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPos = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                if (_startPos == Vector3.zero)
                {
                    _startPos = _cam.ScreenToViewportPoint(Input.mousePosition);
                }
                _currentPos = _cam.ScreenToViewportPoint(Input.mousePosition);

                if (_currentPos.y - _startPos.y > 0)
                {
                    gameObject.layer = 9;
                    _rb.useGravity = false;
                    _mesh.material = _steam;
                }
                else if (_currentPos.y - _startPos.y < 0)
                {
                    _rb.useGravity = true;
                    gameObject.layer = 10;
                    _mesh.material = _ice;
                }
            }
            else
            {
                _mesh.material = _water;
                _rb.useGravity = true;
                gameObject.layer = 8;
            }
        }
    }
    void FixedUpdate()
    {
        if (StaticManager.IsStartGame)
        {
            _rb.AddForce(Vector3.forward * _speed, ForceMode.Acceleration);
            if (!_rb.useGravity)
            {
                _rb.AddForce(Vector3.up * _speedSteem, ForceMode.Acceleration);

                //Vector3 topPos = new Vector3(transform.position.x,_normPos.y+10,transform.position.z);
                //transform.position = Vector3.MoveTowards(transform.position, topPos, 0.1f);
            }
        }
    }
}
