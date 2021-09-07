using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject _thePlayer;

    
    public Canvas _theUI;

    private Player _ps;
    private UIManager _ui;
    private int _floorTarget;

    private void Start()
    {
        if(_thePlayer)
        {
            _ps = _thePlayer.GetComponent<Player>();
        }

        if(_theUI)
        {
            _ui = _theUI.GetComponent<UIManager>();
        }
    }


    public void SetFloor(int x)
    {
        //_ps.SetTargetFloor(x);
        _ps.StartLift(x);
    }

}
