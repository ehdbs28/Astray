using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModule : CommonModule<PlayerController>
{
    [SerializeField]
    private float _gravity = -9.8f;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _sprintSpeed;

    [SerializeField]
    private float _jumpPower;

    private CharacterController _charController;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;

    private float _verticalVelocity;
    private float _jumpVelocity;

    private Vector3 _inputVelocity;

    private bool _isSprint = false;
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _canJump = false;

    private PlayerAnimationModule _animationModule => _controller.GetModule<PlayerAnimationModule>();

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _charController = agentRoot.GetComponent<CharacterController>();
    }

    public override void OnEnterModule(){
        PlayerInputModule inputModule = _controller.GetModule<PlayerInputModule>();

        inputModule.OnMovementKeyPress += SetMovementVelocity;
        inputModule.OnJumpKeyPress += SetJump;
        inputModule.OnSprintKeyPress += SetSprint;
    }

    public override void OnDestroyModule(){
        PlayerInputModule inputModule = _controller.GetModule<PlayerInputModule>();

        inputModule.OnMovementKeyPress -= SetMovementVelocity;
        inputModule.OnJumpKeyPress -= SetJump;
        inputModule.OnSprintKeyPress -= SetSprint;
    }

    public override void OnFixedUpdateModule()
    {
        CalcPlayerMovement();
        CaclJumpVelocity();

        _isGround = _charController.isGrounded;

        if(_isGround == false){
            _verticalVelocity = _jumpVelocity * Time.fixedDeltaTime;
        }
        else{
            _canJump = true;
            _verticalVelocity = _jumpVelocity * 0.3f * Time.fixedDeltaTime;
        }   

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _charController.Move(move);
        _animationModule?.SetGround(_isGround == false);
    }

    private void CaclJumpVelocity()
    {
        if(_isJump){
            _jumpVelocity += Time.fixedDeltaTime * _gravity;
            
            if(_jumpVelocity <= 0f){
                _jumpVelocity = 0;
                _isJump = false;
                _animationModule.SetJump(_isJump);    
            }
        }
        else {
            _jumpVelocity += _gravity * Time.fixedDeltaTime;
            _jumpVelocity = Mathf.Clamp(_jumpVelocity, _gravity, 0);
        }
    }
        
    public void SetJump(){
        if(_canJump == false)
            return;

        _canJump = true;
        _isJump = true;
        _jumpVelocity = _jumpPower;
        _animationModule.SetJump(_isJump);
    }

    public void SetSprint(bool value){
        _isSprint = value;
        _animationModule.SetSprint(_isSprint);
    }

    public void SetMovementVelocity(Vector3 value){
        _inputVelocity = new Vector3(value.x, 0, 0);
        _movementVelocity = new Vector3(value.x, 0, 0);
    } 

    private void CalcPlayerMovement(){
        _inputVelocity.Normalize();
        
        _animationModule?.SetSpeed(_inputVelocity.sqrMagnitude);

        if(_isSprint == false){
            _movementVelocity *= _moveSpeed * Time.fixedDeltaTime;
        }
        else{
            _movementVelocity *= _sprintSpeed * Time.fixedDeltaTime;
        }
    }

    public void StopImmediately(){
        _movementVelocity = Vector3.zero;
        _animationModule?.SetSpeed(0f);
    }

    public override void OnUpdateModule(){}
}
