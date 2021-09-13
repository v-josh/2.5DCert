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

    [SerializeField]
    private bool _moveSideToSide = false;

    //Private Variable
    private Transform _target;
    private bool _switching = false;
    private GameObject _currentSide;

    private bool _startMoving = false;
    private GameObject _currentLiftFloor;



    
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("PointA at: " + _targetA.transform.position + " while PointB is at: " + _targetB.transform.position);

        /*
        if(_targetB != null)
        {
            _target = _targetB;
            //_currentSide = _leftSide;
            
        }*/

        if (_moveSideToSide)
        {
            _maxBufferDistance = (transform.localScale.z / 2.0f);
        }

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

    public bool CheckSides(Transform side)
    {
        if(side == _target)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckLift(Transform floor)
    {
        if(_currentLiftFloor.transform == floor)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetCurrentFloor(GameObject floor)
    {
        
        _currentLiftFloor = floor;
        //Debug.Log("CurrentLiftFloor name is: " + _currentLiftFloor.gameObject.name);
    }

    void MovethePlatform()
    {
        if(_target != null)
        {
            if (_moveSideToSide)
            {
                if (Vector3.Distance(transform.position, _target.localPosition) > _maxBufferDistance)
                {

                    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                }
            }
            else
            {
                Vector3 targetLift = new Vector3(transform.position.x, _target.position.y, transform.position.z);

                if (Vector3.Distance(transform.position, targetLift) > _maxBufferDistance)
                {

                    transform.position = Vector3.MoveTowards(transform.position, targetLift, _speed * Time.deltaTime);
                }
                else
                {
                    
                }

            }
        }
    }

    public bool GetMoveSideToSide()
    {
        return _moveSideToSide;
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
