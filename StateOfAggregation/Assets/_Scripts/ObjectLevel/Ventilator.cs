using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{
    [SerializeField]
    private float _power;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            if (Player.PlayerMain.gameObject.layer==9)
            {
                Player.PlayerMain.Push(transform.forward, _power);
            }
        }
    }
}
