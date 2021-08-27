using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{


    private Vector3 _theHandPos;
    private Vector3 _theStandPos;


    private void Start()
    {
        Debug.Log("Location is: " + gameObject.transform.GetChild(0).position + " while parent is at: " + transform.position);

        Vector3 childPosition = gameObject.transform.GetChild(0).position;
        childPosition.y -= 6.8f;
        childPosition.z -= 0.5f;
        _theHandPos = childPosition;

        childPosition = gameObject.transform.GetChild(0).position;
        childPosition.y += 0.25f;
        childPosition.z += 1.3f;
        _theStandPos = childPosition;

    }

    [SerializeField]
    private Vector3 _handPosition, _standPos;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LedgeGrab")
        {
            Player ps = other.GetComponentInParent<Player>();
            //ps.GrabLedge(_handPosition, this);
            ps.GrabLedge(_theHandPos, this);
        }
    }

    public Vector3 GetStandPos()
    {
        //return _standPos;
        return _theStandPos;
    }
}
