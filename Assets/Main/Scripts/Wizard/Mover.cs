/*using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Wizard_s_Scripts;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _superSpeed;
    [SerializeField] private float _rotate; //не погнятно шо за rotate
    [SerializeField] private bool _isSecondPlayer;
    private bool _isGround;
    private bool _isAnimationPlay;
    private Animator _animator;
    private InputHandler _inputHandler;

    //public HealthContainer HealthContainer { get; private set;}

    private Rigidbody _rigidbody;
    private static readonly int Run = Animator.StringToHash("Run");

    public bool IsSecondPlayer => _isSecondPlayer;


    private void Awake()
    {
        //HealthContainer = GetComponent<HealthContainer>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _inputHandler = new InputHandler();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        var movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        _animator.SetFloat(Run, _isAnimationPlay ? 0.5f : 0f);

        if (!_isSecondPlayer)
        {
            _inputHandler.MoveInput(ref _isAnimationPlay, KeyCode.W, _animator, "Run" ,() =>
            {
                var direction = Vector3.forward * _speed * Time.deltaTime;
                transform.Translate(direction);
            },0.5f);
            
            _inputHandler.MoveInput(ref _isAnimationPlay, KeyCode.S, _animator, "Run",() =>
            {
                var direction = -Vector3.forward * _speed * Time.deltaTime;
                transform.Translate(direction);
            }, 0.5f);
            
            _inputHandler.MoveInput(ref _isAnimationPlay, KeyCode.A, _animator, "Run",() =>
            {
                var direction = -Vector3.right * _speed * Time.deltaTime;
                transform.Rotate(0, _rotate * -1, 0);
            }, 0.5f);
            
            _inputHandler.MoveInput(ref _isAnimationPlay, KeyCode.D, _animator, "Run",() =>
            {
                var direction = Vector3.right * _speed * Time.deltaTime;
                transform.Rotate(0, _rotate, 0);
            }, 0.6f);
            
            _inputHandler.MoveInput(ref _isAnimationPlay, KeyCode.LeftShift, _animator, "Run",() =>
            {
                var direction = Vector3.forward * _superSpeed * Time.deltaTime;
                transform.Translate(direction);
            }, );


            //Player 1 Jumper
            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                _rigidbody.AddForce(new Vector3(0, 380, 0));
            }
        }
        else
        {
            //player 2 controller
            if (Input.GetKey(KeyCode.UpArrow))
            {
                var direction = Vector3.forward * _speed * Time.deltaTime;
                transform.Translate(direction);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                var direction = -Vector3.forward * _speed * Time.deltaTime;
                transform.Translate(direction);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                var direction = -Vector3.right * _speed * Time.deltaTime;
                transform.Rotate(0, _rotate * -1, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                var direction = Vector3.right * _speed * Time.deltaTime;
                transform.Rotate(0, _rotate, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }
}*/