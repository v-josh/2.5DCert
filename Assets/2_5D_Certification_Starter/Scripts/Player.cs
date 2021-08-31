using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed = 5.0f;

    [SerializeField]
    private float _jumpHeight = 7.0f;

    [SerializeField]
    private float _gravity = 2.0f;

    [SerializeField]
    private GameObject _spawnFromLadder;



    private Vector3 _direction;
    private CharacterController _cc;
    private Animator _anim;
    private bool _hasJumped = false;

    private bool _grabbedLedge = false;
    private LedgeGrab _activeLedge;

    private bool _ladderGrab = false;
    private GameObject _ladderSpawn;
    private bool _eToLadder = false;

    private bool _spawnToFloor = false;
    private Vector3 _originalPos;
    private bool _finalLadderCheck = false;
    private bool _onTopOfLadder = false;
    private float _verticalAxisInput = 0f;

    private float _originalGravity;
    private bool _changeGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _cc = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();

        _originalGravity = _gravity;

        if(_cc.isGrounded == false)
        {
            _direction.y -= _gravity;
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        if (!_eToLadder)
        {

            CalculateMovement();
        }
        else
        {
            if (!_onTopOfLadder)
            {
                VerticalMovemnt();
            }

        }

        if(Input.GetKeyDown(KeyCode.E) && _grabbedLedge == true)
        {
            _anim.SetBool("ClimbUp", true);
        }

        
        if (Input.GetKeyDown(KeyCode.E) && _ladderGrab == true)
        {
            if (!_eToLadder)
            {
                LadderGrab();
                //_anim.SetBool("LadderGrab", true);
                _eToLadder = true;
            }
        }

        if (_finalLadderCheck == true)
        {
            if (transform.position.x != 0)
            {
                Vector3 thePos = transform.position;
                thePos.x = 0f;
                transform.position = thePos;
                _finalLadderCheck = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("Player Facing: " + transform.rotation.y);
            _ladderSpawn = null;
        }

        if(_spawnToFloor)
        {
            GetOffLadder();
        }

    }

    void CalculateMovement()
    {
        
        if (_cc.isGrounded == true)
        {
            
            if(_changeGravity)
            {
                _changeGravity = false;
            }
            
            if (_hasJumped)
            {
                _anim.SetBool("Jumping", false);
            }

            if (_cc.isGrounded && _direction.y < 0f)
            {
                _direction.y = 0f;
            }


            float h = Input.GetAxisRaw("Horizontal");



            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;  //if the direction.z is greater than 0, then set facing.y to 0; else, set facing.y to 180
                transform.localEulerAngles = facing;


            }

            _anim.SetFloat("speed", Mathf.Abs(h));
            _direction = new Vector3(0, 0, h) * _playerSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _anim.SetBool("Jumping", true);
                _hasJumped = true;
            }
            
            
        }
        else
        {
            if (!_changeGravity)
            {
                _direction.y -= _gravity * Time.deltaTime;
            }
            else
            {
                //_gravity = 100f;
                _direction.y -= _gravity * Time.deltaTime * 100f;
                //_gravity = _originalGravity;
                //_changeGravity = false;
                _onTopOfLadder = false;
            }
                
        }



        _cc.Move(_direction * Time.deltaTime);
    }

    void VerticalMovemnt()
    {
        _verticalAxisInput = Input.GetAxisRaw("Vertical");
        _anim.SetFloat("LadderSpeed", Mathf.Abs(_verticalAxisInput));
        _direction = new Vector3(0, _verticalAxisInput, 0) * _playerSpeed;

        if (_verticalAxisInput != 0)
        {
            _anim.enabled = true;
            _cc.Move(_direction * Time.deltaTime);
        }
        else
        {
            _anim.enabled = false;
        }
    }

    public void GrabLedge(Vector3 hands, LedgeGrab cur)
    {
        _cc.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("speed", 0.0f);
        transform.position = hands;

        _grabbedLedge = true;

        _activeLedge = cur;

    }


    void LadderGrab()
    {
        float theY = transform.rotation.y;


        //transform.rotation.y
        if ( theY < 0.9f)
        {
            transform.rotation = Quaternion.identity;

            transform.Rotate(0f, Mathf.Abs(90f), 0f);
        }
        else
        {
            transform.Rotate(0f, 270f, 0f);
        }

        _originalPos = transform.position;
        transform.position = _ladderSpawn.transform.position;
        _anim.SetBool("LadderClimb", true);


    }

    public void SetLadder(bool x)
    {
        _ladderGrab = x;
    }

    public void GetLadderSpawn(GameObject obj)
    {
        _ladderSpawn = obj;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _anim.SetBool("ClimbUp", false);
        _cc.enabled = true;
    }

    public void OffLadder(GameObject go)
    {
        
        _ladderGrab = false;
        _spawnToFloor = true;
        _spawnFromLadder = go;

    }

    void GetOffLadder()
    {

        _anim.SetBool("LadderClimb", false);

        
        transform.Rotate(0f, transform.rotation.y - 90f, 0f);

        
        transform.position = _spawnFromLadder.transform.position;
        Vector3 pos = transform.position;
        pos.x = 0f;
        transform.position = pos;

        
        _ladderGrab = false;
        _spawnToFloor = false;
        _eToLadder = false;
        _finalLadderCheck = true;
        
    }

    public void SetTopLadder(bool x)
    {
        _onTopOfLadder = x;
        _cc.enabled = false;
    }

    public void TopLadder(GameObject go)
    {
        Vector3 thePos = transform.position;
        thePos.z -= 0.4f;
        transform.position = thePos;
        transform.Rotate(0f, 180f, 0f);
        _spawnFromLadder = go;

        _anim.SetBool("LadderTop", true);             //Enable this line while disable the next line to enable climbing up animation
        //LadderComplete();

        //_anim.SetBool("LadderClimb", false);
        //_anim.enabled = true;


    }

    public void LadderComplete()
    {
        _onTopOfLadder = true;

        transform.position = _spawnFromLadder.transform.position;

        Vector3 pos = transform.position;
        pos.x = 0f;
        transform.position = pos;

        transform.Rotate(0f, transform.rotation.y - 90f, 0f);
        _anim.SetBool("LadderTop", false);
        _anim.SetBool("LadderClimb", false);
        _ladderGrab = false;
        _spawnToFloor = false;
        _eToLadder = false;
        _finalLadderCheck = false;
        _changeGravity = true;
        _cc.enabled = true;
    }


}
