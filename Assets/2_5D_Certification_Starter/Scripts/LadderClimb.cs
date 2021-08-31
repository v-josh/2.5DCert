using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{

    [SerializeField]
    private GameObject _bottomSpawnLoc;

    private Player _ps;

    private bool _pressedE = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            /*
            if(!_pressedE)
            { 
                _ps = other.GetComponentInParent<Player>();
                /*
                _ps.LadderGrab(_bottomSpawnLoc);
                _ps.SetLadder(true);
                _pressedE = true;
                
            }
        */

            _ps = other.GetComponentInParent<Player>();
            _ps.GetLadderSpawn(_bottomSpawnLoc);
            _ps.SetLadder(true);

            //Debug.Log("Player Position is: " + other.transform.position);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //_pressedE = false;
            if (_ps != null)
            {
                _ps.SetLadder(false);
            }
        }
    }


    

}
