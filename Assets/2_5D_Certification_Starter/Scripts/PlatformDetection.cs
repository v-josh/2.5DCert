using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public enum Sides { Left, Right};
    public Sides _currentSide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MovingPlatform")
        {
            Terminal tx = other.transform.GetComponentInChildren<Terminal>();
            if(tx != null)
            {
                if(_currentSide == Sides.Left)
                {
                    tx.SetCurrentSide(0);
                }
                else
                {
                    tx.SetCurrentSide(1);
                }
            }

            //Player ps = other.transform.GetComponentInChildren<Player>();
            Player ps = GameObject.FindWithTag("Player").GetComponent<Player>();
            if(ps != null)
            {
                ps.EnableCC();
            }
        }
    }
}
