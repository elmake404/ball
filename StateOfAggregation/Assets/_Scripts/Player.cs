using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerMain;

    private Vector3 direction;
    [SerializeField]
    private Guide _target;
    [SerializeField]
    private Collider _colliderMain;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Material _waterMaterial, _steamMaterial, _iceMaterial;
    [SerializeField]
    private MeshRenderer _meshMain;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _buffSpeed;
    private bool _isOnTheGround;

    void Awake()
    {
        StaticManager.IsStartGame = true;
        _buffSpeed = 0;
        PlayerMain = this;
    }
    void FixedUpdate()
    {
        if (StaticManager.IsStartGame)
        {
            if (gameObject.layer != 9)
            {
                if (_isOnTheGround)
                {
                    direction = (_target.transform.position - transform.position).normalized;
                    _rb.AddForce(direction * (_speed + _buffSpeed), ForceMode.Acceleration);
                }
                else
                {
                    _rb.AddForce(Vector3.forward*2, ForceMode.Acceleration);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            _rb.velocity = Vector3.zero;
            StaticManager.IsStartGame = false;
            StaticManager.IsGameWin = true;
        }
        if (other.tag == "Abyss")
        {
            StaticManager.IsGameOver = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            if (!_isOnTheGround)
            {
                _target.StandNextToMe();
            }
            _isOnTheGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            _isOnTheGround = false;
        }
    }
    public void Push(Vector3 Direction, float Power)
    {
        _rb.AddForce(Direction * Power, ForceMode.Acceleration);
    }
    public void BoostSpeed(float boostSpeed)
    {
        _buffSpeed += boostSpeed;
    }
    public void LesseningSpeed(float lesseningSpeed)
    {
        _buffSpeed -= lesseningSpeed;
    }
    public void Destruction()
    {
        StaticManager.IsStartGame = false;
        StaticManager.IsGameOver = true;

        _meshMain.enabled = false;
        _rb.velocity = Vector3.zero;
    }
    public void Water()
    {
        gameObject.layer = 8;
        _colliderMain.isTrigger = false;
        _rb.useGravity = true;
        //_rb.drag = 1.7f;
        _meshMain.material = _waterMaterial;
    }
    public void Ice()
    {
        gameObject.layer = 10;
        _colliderMain.isTrigger = false;
        //_rb.drag = 1;
        _rb.useGravity = true;
        _meshMain.material = _iceMaterial;
    }
    public void Steam()
    {
        gameObject.layer = 9;
        _colliderMain.isTrigger = true;
        _rb.useGravity = false;
        //_rb.drag = 1.7f;

        _meshMain.material = _steamMaterial;
    }
}
