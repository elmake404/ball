using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private CanvasManager _canvasManager;
    [SerializeField]
    private Rigidbody _rb;
    private Vector3 _startPos = new Vector3(0.5f, 0.5f, 0), _currentPos = new Vector3(0.5f, 0.5f, 0), _normPos;
    private Camera _cam;
    [SerializeField]
    private Material _water, _steam, _ice;
    [SerializeField]
    private MeshRenderer _mesh;

    private bool _isCoolOff, _isEvaporate, _isDoNotRecord;
    [SerializeField]
    private float _speed, _speedSteem;
    void Start()
    {
        _isCoolOff = false;
        _isEvaporate = false;
        _isDoNotRecord = false;

        _mesh.material = _water;
        _normPos = transform.position;
        _cam = Camera.main;
    }
    private void Update()
    {
        if (StaticManager.IsStartGame)
        {
            if (_isCoolOff)
            {
                _isDoNotRecord = true;

                _isCoolOff = false;
                _startPos = _currentPos;
            }
            if (_isEvaporate)
            {
                _isDoNotRecord = true;

                _isEvaporate = false;
                Vector3 viewportPoint = _currentPos;
                _startPos = viewportPoint - new Vector3(0, 0.045f, 0);
            }

            if (Mathf.Abs(_currentPos.y - _startPos.y) < 0.035f)
            {
                _mesh.material = _water;
                _rb.useGravity = true;
                gameObject.layer = 8;

            }
            else if (_currentPos.y - _startPos.y > 0)
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

            if (Input.GetMouseButtonDown(0))
            {
                if (!_isDoNotRecord)
                {
                    _startPos = _cam.ScreenToViewportPoint(Input.mousePosition);
                }
            }
            if (Input.GetMouseButton(0))
            {
                _isDoNotRecord = false;

                if (_startPos == Vector3.zero)
                {
                    _startPos = _cam.ScreenToViewportPoint(Input.mousePosition);
                }
                _currentPos = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _startPos = _currentPos;
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
            }

            if (gameObject.layer == 10)
            {
                if (_canvasManager.TimeIce() <= 0)
                {
                    _isCoolOff = true;
                }
            }
            if (gameObject.layer == 9)
            {
                if (_canvasManager.TimeSteam() <= 0)
                {
                    _isCoolOff = true;
                }
            }
            if (gameObject.layer == 8)
            {
                _canvasManager.Recovery();
            }
        }
    }
    public void Push(Vector3 Direction, float Power)
    {
        _rb.AddForce(Direction * Power, ForceMode.Acceleration);
    }
    public void Destruction()
    {
        _mesh.enabled = false;
        _rb.velocity = Vector3.zero;
    }
    public void ExternalEffects()
    {
        if (gameObject.layer == 10)
        {
            _isCoolOff = true;
        }
        else if (gameObject.layer == 8)
        {
            _isEvaporate = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            StaticManager.IsGameWin = true;
        }
        if (other.tag == "Abyss")
        {
            StaticManager.IsGameOver = true;
        }
    }
}
