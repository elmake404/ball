using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lattice : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StaticManager.IsStartGame = false;
            StaticManager.IsGameOver = true;

            other.GetComponent<PlayerControl>().Destruction();
        }
    }

}
