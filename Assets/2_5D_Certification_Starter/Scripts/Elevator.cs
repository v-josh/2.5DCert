using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float _liftSpeed = 2.0f;

    [SerializeField]
    private float _delayBeforeMoving = 5.0f;

    [SerializeField]
    private Transform _target;

    private float _maxBufferDistance = 0.5f;
    private bool _startMoving = false;

    private void Start()
    {
        _maxBufferDistance = (transform.localScale.z / 2.0f);
    }



    void MovethePlatform()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.localPosition) > _maxBufferDistance)
            {

                transform.position = Vector3.MoveTowards(transform.position, _target.position, _liftSpeed * Time.deltaTime);
            }
        }
    }

}
