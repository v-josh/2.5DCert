using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform _targetA;

    [SerializeField]
    private Transform _targetB;

    [SerializeField]
    private GameObject _leftSide, _rightSide;

    [SerializeField]
    private bool _isAtPointB = false;

    [SerializeField]
    private float _speed = 20f;

    [SerializeField]
    private float _maxBufferDistance = 0.5f;

    //Private Variable
    private Transform _target;
    private bool _switching = false;
    private GameObject _currentSide;

    private bool _startMoving = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("PointA at: " + _targetA.transform.position + " while PointB is at: " + _targetB.transform.position);

        if(_targetB != null)
        {
            _target = _targetB;
            //_currentSide = _leftSide;
            
        }

        _maxBufferDistance = (transform.localScale.z / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_target != null)
        {

            if (Vector3.Distance(transform.position, _target.localPosition) > _maxBufferDistance)
            {

                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            }
            else
            {
                if (_isAtPointB)
                {
                    _target = _targetA;
                    _isAtPointB = false;
                    //_currentSide = _rightSide;
                }
                else
                {
                    _target = _targetB;
                    _isAtPointB = true;
                    //_currentSide = _leftSide;
                }
            }
        }*/

        if(_startMoving)
        {
            MovethePlatform();
        }

    }


    public void MoveToSide(Transform side)
    {
        _target = side;
        _startMoving = true;
    }

    void MovethePlatform()
    {
        if(_target != null)
        {
            if (Vector3.Distance(transform.position, _target.localPosition) > _maxBufferDistance)
            {

                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            }
        }
    }


    



    private void FixedUpdate()
    {
        /*
        if (_switching == false)
        {

            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }

        if (transform.position == _targetB.position)
        {
            _switching = true;
        }
        else if (transform.position == _targetA.position)
        {
            _switching = false;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
