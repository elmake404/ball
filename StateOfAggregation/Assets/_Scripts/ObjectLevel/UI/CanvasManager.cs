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
        if (StaticManager.IsGameOver || StaticManager.IsGameWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
    public void RecoverySteamExtra()
    {
        if (_fillSteam.fillAmount < 0.5f)
        {
            _fillSteam.fillAmount = 0.5f;
        }
    }
    public void RecoveryIce()
    {
        if (_fillIce.fillAmount < 1)
        {
            _fillIce.fillAmount += 0.01f;
        }
    }
    public void RecoverySteem()
    {
        if (_fillSteam.fillAmount < 1)
        {
            _fillSteam.fillAmount += 0.01f;
        }
    }
    public bool GetIce()
    {
        if (_fillIce.fillAmount>=0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetSteam()
    {
        if (_fillSteam.fillAmount>=0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
