using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Image _fillIce, _fillSteam;

    private void Awake()
    {
        StaticManager.IsGameOver = false;
        StaticManager.IsGameWin = false;
    }
    void Start()
    {
        StaticManager.IsStartGame = true;

    }

    void Update()
    {
        if (StaticManager.IsGameOver||StaticManager.IsGameWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void FixedUpdate()
    {

    }
    public float TimeIce()
    {
        if (_fillIce.fillAmount > 0)
        {
            _fillIce.fillAmount -= 0.005f;
        }

        return _fillIce.fillAmount;
    }
    public float TimeSteam()
    {
        if (_fillSteam.fillAmount > 0)
        {
            _fillSteam.fillAmount -= 0.005f;
        }

        return _fillSteam.fillAmount;
    }
    public void Recovery()
    {
        if (_fillSteam.fillAmount < 1)
        {
            _fillSteam.fillAmount += 0.01f;
        }
        if (_fillIce.fillAmount < 1)
        {
            _fillIce.fillAmount += 0.01f;
        }
    }
}
