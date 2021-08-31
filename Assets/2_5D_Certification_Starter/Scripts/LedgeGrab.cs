using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{

    [SerializeField]
    private float _sizeMultiplier = 1f;

    private Vector3 _theHandPos;
    private Vector3 _theStandPos;
    private bool _jumpedToReached = false;


    private void Start()
    {
        //Debug.Log("Location is: " + gameObject.transform.GetChild(0).position + " while parent is at: " + transform.position);

        Vector3 childPosition = gameObject.transform.GetChild(0).position;
        childPosition.y -= 6.8f;
        childPosition.y += (_sizeMultiplier / 10f) + 0.05f;
        childPosition.z -= ((0.5f * _sizeMultiplier) + (_sizeMultiplier / 10f));
        _theHandPos = childPosition;

        childPosition = gameObject.transform.GetChild(0).position;
        childPosition.y += 0.25f;
        childPosition.z += 1.3f;
        _theStandPos = childPosition;
        //Debug.Log("Hand Position is: " + _theHandPos);

    }

    [SerializeField]
    private Vector3 _handPosition, _standPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LedgeGrab")
        {
            Vector3 pos = other.transform.position;
            if(pos.y < transform.position.y)
            {
                _jumpedToReached = true;
                //Debug.Log("Jumped To Reach is True!");
            }
            else
            {
                _jumpedToReached = false;
                //Debug.Log("Jumped To Reach is FALSE!");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LedgeGrab")
        {
            if (_jumpedToReached == true)
            {
                Player ps = other.GetComponentInParent<Player>();
                //ps.GrabLedge(_handPosition, this);
                ps.GrabLedge(_theHandPos, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        //return _standPos;
        return _theStandPos;
    }
}
