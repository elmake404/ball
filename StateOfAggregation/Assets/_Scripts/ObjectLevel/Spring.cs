using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float _forseSpring;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * _forseSpring, ForceMode.Acceleration);
        }

    }
}
