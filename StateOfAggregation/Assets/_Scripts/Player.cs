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
    private Rigidbody _rb;
    [SerializeField]
    private Material _water, _steam, _ice;
    [SerializeField]
    private MeshRenderer _mesh;

    [SerializeField]
    private float _speed;
    private bool _isOnTheGround;

    void Awake()
    {
        PlayerMain = this;
        //velocity = _rb.velocity;
    }

    void FixedUpdate()
    {
        if (_isOnTheGround)
        {
            direction = (_target.transform.position - transform.position).normalized;
            _rb.AddForce(direction * _speed, ForceMode.Acceleration);
        }
        //else
        //{
        //    _rb.AddForce(Vector3.forward * _speed, ForceMode.Acceleration);
        //}
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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            if (!_isOnTheGround)
            {
                _target.StandNextToMe();
            }
            _isOnTheGround = true;
            //Debug.Log(_isOnTheGround);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            _isOnTheGround = false;
            //Debug.Log(_isOnTheGround);
        }
    }

}
