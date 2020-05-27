using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    [SerializeField]
    private bool _isGrating;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_isGrating)
            {
                StaticManager.IsStartGame = false;
                other.isTrigger = true;
            }
            else
            {
                if (other.gameObject.layer==8)
                {
                    StaticManager.IsStartGame = false;
                    other.isTrigger = true;
                }
            }
        }
    }
}
