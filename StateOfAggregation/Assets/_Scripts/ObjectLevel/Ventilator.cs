using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    private float _power;
    [SerializeField]
    private bool _suck;
    private void Start()
    {
        if (_suck)
        {
            _direction = -transform.forward;
        }
        else
        {
            _direction = transform.forward;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            other.GetComponent<Rigidbody>().AddForce(_direction * _power,ForceMode.Acceleration);
        }
    }
}
