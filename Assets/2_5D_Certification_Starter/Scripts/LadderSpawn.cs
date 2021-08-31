using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSpawn : MonoBehaviour
{

    [SerializeField]
    private bool _spawnDownstairs = true;

    [SerializeField]
    private GameObject _spawnLoc;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            if (_spawnDownstairs == true)
            {
                if(other.transform.position.y > transform.position.y)
                {
                    //Debug.Log("Collision with " + other.tag + " from top to bottom!");
                    Player ps = other.GetComponentInParent<Player>();

                    ps.OffLadder(_spawnLoc);
                }
            }
            else
            {
                //Debug.Log("Inside Ladder Spawn where _spawnDownstairs is false");
;

                /*
                if (other.transform.position.y < transform.position.y)
                {





                    //Debug.Log("Collision with " + other.tag + " from bottom to top! asdfasdfsdf");
                    //Animator anim = 


                }
                */
            }
        }
        else if (other.tag == "LadderGrab")
        {
            Player ps = other.GetComponentInParent<Player>();
            ps.SetTopLadder(true);
            ps.TopLadder(_spawnLoc);
        }
        


    }

}
