using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public enum Position { Left, Right, Middle };

    public Position _terminalPosition;

    [SerializeField]
    private GameObject _movingPlatform;

    [SerializeField]
    private Transform _leftSide, _rightSide;


    private Transform _platformSide;
    private bool _theMiddle = false;
    private bool _middlePress = true;

    private Position _currentPosition;

    private bool _onLeft = false;
    private bool _onRight = false;

    private void Start()
    {
        if(_terminalPosition == Position.Left)
        {
            _platformSide = _leftSide;
            _onLeft = true;
        }
        else if(_terminalPosition == Position.Right)
        {
            _platformSide = _rightSide;
            _onRight = false;
        }
        else
        {
            _theMiddle = true;
            //Debug.Log("The MIddle is now true");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player ps = other.GetComponentInParent<Player>();
            MovingPlatform mp = _movingPlatform.GetComponent<MovingPlatform>();

            ps.TerminalContact(true);

            if (_theMiddle == false)
            {
                ps.MoveThePlatform(_platformSide, mp);
            }
            else
            {

                if(_currentPosition == Position.Left)
                {
                    _platformSide = _rightSide;
                    ps.MoveThePlatform(_platformSide, mp);
                }
                else if (_currentPosition == Position.Right)
                {
                    _platformSide = _leftSide;
                    ps.MoveThePlatform(_platformSide, mp);
                }
                
                /*
                Debug.Log("Inside Middle True");
                if(_onLeft == true)
                {
                    _platformSide = _rightSide;
                    ps.MoveThePlatform(_platformSide, mp);
                }
                else if (_onRight)
                {
                    _platformSide = _leftSide;
                    ps.MoveThePlatform(_platformSide, mp);
                }*/

                //Grab the position from the enum
                //Then switch it
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Player ps = other.GetComponentInParent<Player>();
            ps.TerminalContact(false);
        }
    }

    public void SetCurrentSide(int x)
    {
        if(x == 0) // left
        {
            _currentPosition = Position.Left;
        }
        else if (x == 1) //right
        {
            _currentPosition = Position.Right;
        }
    }
}
