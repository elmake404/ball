using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerMain;
    Vector3 direction;
    public Transform Target;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Material _water, _steam, _ice;
    [SerializeField]
    private MeshRenderer _mesh;

    void Awake()
    {
        PlayerMain = this;
        //velocity = _rb.velocity;
    }

    private void Update()
    {
    }
    void FixedUpdate()
    {
        direction = (Target.position - transform.position).normalized;
        _rb.AddForce(direction * 50, ForceMode.Acceleration);
        //Debug.Log(_rb.spee);

        //_rb.velocity  = velocity;
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
