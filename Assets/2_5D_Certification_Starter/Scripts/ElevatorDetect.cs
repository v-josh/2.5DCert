using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Elevator")
        {
            Player ps = other.GetComponentInChildren<Player>();
            MovingPlatform mp = other.GetComponent<MovingPlatform>();
            
            if(ps != null)
            {
                GameObject checkFloor = ps.CheckLiftTarget();
                if (checkFloor == this.gameObject)
                {
                    ps.EnableCC();
                }
            }

            if(mp != null)
            {
               mp.SetCurrentFloor(this.gameObject);

            }
        }
    }
}
