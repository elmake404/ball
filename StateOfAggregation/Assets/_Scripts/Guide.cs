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
        //Debug.Log((transform.position - _player.transform.position).normalized);
        if ((transform.position - _player.transform.position).magnitude < 1.5f && _progress != 1)
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
                if ((transform.position - _player.transform.position).magnitude >= 2)
                {
                    //RaycastHit hitNear;
                    //Physics.Raycast(transform.position, -transform.up, out hitNear, 5f);
                    //transform.up = hitNear.normal;

                    //transform.position = hitNear.point /*+ Vector3.up/1.4f*/ ;

                    break;
                }
            }
        }
    }
    public void StandNextToMe()
    {
        float Magnitude = (transform.position - _player.transform.position).magnitude;

        if (Magnitude <= 1f)
        {
            return;
        }

        Vector3 OldPos = transform.position;

        while (true)
        {
            _progress += _duration/10;
            if (_progress > 1f)
            {
                _progress = 1f;
                break;
            }
            transform.position = _spline.GetPoint(_progress);

            if ((transform.position - OldPos).magnitude >= Magnitude)
            {
                break;
            }

        }

    }
}
