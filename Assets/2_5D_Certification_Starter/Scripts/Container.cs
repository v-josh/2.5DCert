using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    private bool _canMove = false;
    private float _playerDirection = 0f;
    private CharacterController _cc;
    private float _pushSpeed = 5.0f;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Hitting Playter");
            Player ps = other.gameObject.GetComponentInParent<Player>();
            if(ps != null)
            {
                _canMove = true;
                ps.PushStart();
                _playerDirection = ps.GetPlayerDirection();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            // Debug.Log("Player left collider");
            Player ps = other.gameObject.GetComponentInParent<Player>();
            if (ps != null)
            {
                _canMove = false;
                ps.PushEnd();
                _playerDirection = 0f;
            }
        }
    }

    private void Update()
    {
        if(_canMove)
        {

        }


    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(0f, 0f, _playerDirection);
        _cc.Move(dir * _pushSpeed * Time.deltaTime);
    }

}
