using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            StaticManager.IsStartGame = false;
            other.isTrigger = true;
        }
    }
}
