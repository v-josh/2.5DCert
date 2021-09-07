using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _gameManager;

    [SerializeField]
    private Image _elevatorCanvas;

    [SerializeField]
    private Button _floorOne;

    [SerializeField]
    private Button _floorTwo;

    //private int _floorTarget;

    private GameManager _gM;


    private void Start()
    {
        if(_elevatorCanvas != null)
        {
            _elevatorCanvas.gameObject.SetActive(false);
        }

        if(_gameManager)
        {
            _gM = _gameManager.GetComponent<GameManager>();
        }
    }

    public void SetElevatorMenu(bool x)
    {

        _elevatorCanvas.gameObject.SetActive(x);

    }

    public void FloorOne()
    {
       // _floorTarget = 1;
        _gM.SetFloor(1);
    }

    public void FloorTwo()
    {
        //_floorTarget = 2;
        _gM.SetFloor(2);

    }
    

}
