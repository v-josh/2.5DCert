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
    private CinemachineVirtualCamera _cmRight;

    [SerializeField]
    private CinemachineVirtualCamera _cmLeft;

    private Vector3 _direction;
    private CharacterController _cc;
    private Animator _anim;
    private bool _hasJumped = false;
    private bool _reverseRun = false;

    private bool _grabbedLedge = false;
    private LedgeGrab _activeLedge;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _cc = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();

        if(_cc.isGrounded == false)
        {
            _direction.y -= _gravity;
        }



    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.E) && _grabbedLedge == true)
        {
            _anim.SetBool("ClimbUp", true);
        }

    }

    void CalculateMovement()
    {
        if (_cc.isGrounded == true)
        {
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
            _direction.y -= _gravity * Time.deltaTime;
        }

        _cc.Move(_direction * Time.deltaTime);
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

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _anim.SetBool("ClimbUp", false);
        _cc.enabled = true;
    }


}
