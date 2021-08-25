using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("Location is: " + transform.position);
    }

    [SerializeField]
    private Vector3 _handPosition, _standPos;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "LedgeGrab")
        {
            Player ps = other.GetComponentInParent<Player>();
            ps.GrabLedge(_handPosition, this);
        }
    }

    public Vector3 GetStandPos()
    {
        return _standPos;
    }
}
