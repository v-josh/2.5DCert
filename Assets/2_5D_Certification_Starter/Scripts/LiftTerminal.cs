using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTerminal : MonoBehaviour
{

    [SerializeField]
    private float _delayBeforeMoving = 5.0f;

    [SerializeField]
    private GameObject _elevatorLift;

    [SerializeField]
    private GameObject[] _floorCollider;

    [SerializeField]
    private int _onFloor = 1;

    private MovingPlatform _mp;

    private void Start()
    {
        _mp = _elevatorLift.GetComponent<MovingPlatform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player ps = other.GetComponentInParent<Player>();
            ps.TerminalContact(true);
            ps.SetLiftFloors(_floorCollider);
            //ps.MoveThePlatform(_floorCollider[0].transform, _mp);
            ps.GetMovingPlat(_mp);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player _ps = other.GetComponentInParent<Player>();
            _ps.TerminalContact(false);
        }
    }
}
