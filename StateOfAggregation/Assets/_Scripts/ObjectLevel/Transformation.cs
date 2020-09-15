using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    [SerializeField]
    private bool _isWterState, _isIceState, _isStemState;
    private bool _isPlayerStands;
    private Player _player;
    private void Start()
    {
        _player = Player.PlayerMain;
    }
    private void Update()
    {
        if (_isPlayerStands && Input.GetMouseButtonDown(0))
        {
            if (_isWterState)
            {
                _player.Water();
            }
            else if (_isIceState)
            {
                _player.Ice();
            }
            else
            {
                _player.Steam();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerStands = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerStands = false;
        }
    }
}
