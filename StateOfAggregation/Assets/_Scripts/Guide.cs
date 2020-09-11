using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField]
    private BezierSpline _spline;
    private Player _player;
    [SerializeField]
    private float _duration;
    private float _progress;
    void Start()
    {
        transform.position = _spline.GetPoint(0);

        _player = Player.PlayerMain;
    }

    void FixedUpdate()
    {
        //Debug.Log((transform.position - _player.transform.position).magnitude);
        if ((transform.position - _player.transform.position).magnitude < 1f && _progress != 1)
        {
            while (true)
            {
                _progress += _duration;
                if (_progress > 1f)
                {
                    _progress = 1f;
                    break;
                }
                transform.position = _spline.GetPoint(_progress);
                if ((transform.position - _player.transform.position).magnitude >= 3)
                {
                    break;
                }
            }
        }
    }
}
