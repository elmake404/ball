using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float _forseSpring;
    [SerializeField]
    private Vector3 _direction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Player.PlayerMain.gameObject.layer == 10)
            {
                Player.PlayerMain.Push(_direction, _forseSpring);
            }
            else
            {
                Player.PlayerMain.Destruction();
            }
        }
    }
}
