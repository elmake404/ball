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
    private float _speed, _speedSteem, _timeState;
    private float _timeWater, _timeSteam, _timeIce;
    void Start()
    {
        _isCoolOff = false;
        _isEvaporate = false;
        _isDoNotRecord = false;

        _timeWater = _timeState;
        _timeSteam = _timeState;
        _timeIce = _timeState;
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

                _timeIce = _timeState;
                _timeWater = _timeState;
                _timeSteam = _timeState;

                if (_timeWater <= 0)
                {
                    _timeSteam = _timeState;
                    _timeIce = _timeState;

                    _mesh.material = _water;
                    _rb.useGravity = true;
                    gameObject.layer = 8;
                }
                else
                {
                    _timeWater -= Time.deltaTime;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (StaticManager.IsStartGame)
        {
            if (Mathf.Abs(_currentPos.y - _startPos.y) < 0.035f)
            {
                if (_timeWater <= 0)
                {
                    _timeSteam = _timeState;
                    _timeIce = _timeState;

                    _mesh.material = _water;
                    _rb.useGravity = true;
                    gameObject.layer = 8;
                }
                else
                {
                    _timeWater -= Time.deltaTime;
                }
            }
            else if (_currentPos.y - _startPos.y > 0 && _canvasManager.GetSteam())
            {
                if (gameObject.layer == 10)
                {
                    if (_timeWater <= 0)
                    {
                        _timeSteam = _timeState;
                        _timeIce = _timeState;

                        _mesh.material = _water;
                        _rb.useGravity = true;
                        gameObject.layer = 8;
                    }
                    else
                    {
                        _timeWater -= Time.deltaTime;
                    }

                }
                else
                {
                    if (_timeSteam <= 0)
                    {
                        _timeIce = _timeState;
                        _timeWater = _timeState;

                        gameObject.layer = 9;
                        _rb.useGravity = false;
                        _mesh.material = _steam;
                    }
                    else
                    {
                        _timeSteam -= Time.deltaTime;
                    }
                }
            }
            else if (_currentPos.y - _startPos.y < 0 && _canvasManager.GetIce())
            {
                if (gameObject.layer == 9)
                {
                    if (_timeWater <= 0)
                    {
                        _timeSteam = _timeState;
                        _timeIce = _timeState;

                        _mesh.material = _water;
                        _rb.useGravity = true;
                        gameObject.layer = 8;
                    }
                    else
                    {
                        _timeWater -= Time.deltaTime;
                    }

                }
                else
                {
                    if (_timeIce <= 0)
                    {
                        _timeWater = _timeState;
                        _timeSteam = _timeState;

                        _rb.useGravity = true;
                        gameObject.layer = 10;
                        _mesh.material = _ice;
                    }
                    else
                    {
                        _timeIce -= Time.deltaTime;
                    }
                }
            }

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

            if (gameObject.layer != 10)
            {
                _canvasManager.RecoveryIce();
            }
            if (gameObject.layer != 9)
            {
                _canvasManager.RecoverySteem();
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
            _canvasManager.RecoverySteamExtra();
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
