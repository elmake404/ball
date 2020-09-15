using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpeed : MonoBehaviour
{
    [SerializeField]
    private float _namberBoostSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.PlayerMain.BoostSpeed(_namberBoostSpeed);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.PlayerMain.LesseningSpeed(_namberBoostSpeed);
        }

    }

}
