using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBarrier : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _gateway;
    private void Start()
    {
        if (_gateway.Length == 0)
        {
            enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enabled = false;
            if (other.gameObject.layer == 10)
            {
                for (int i = 0; i < _gateway.Length; i++)
                {
                    Destroy(_gateway[i]);
                }
            }
            else if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                StaticManager.IsStartGame = false;
                StaticManager.IsGameOver = true;
                other.GetComponent<PlayerControl>().Destruction();
            }

        }
    }
}
