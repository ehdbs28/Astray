using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModule : CommonModule<PlayerController>
{
    private CharacterController _charController;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;

    private float _verticalVelocity;
    private float _jumpVelocity;

    private Vector3 _inputVelocity;

    private bool _isSprint = false;
    private bool _isGround = false;
    private bool _isJump = false;

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

    public override void OnUpdateModule(){
        _isGround = _charController.isGrounded;
    }

    public override void OnFixedUpdateModule()
    {
        CalcPlayerMovement();
        CaclJumpVelocity();

        if(_isGround == false){
            _verticalVelocity = _jumpVelocity * Time.fixedDeltaTime;
        }
        else{
            _verticalVelocity = _jumpVelocity * 0.3f * Time.fixedDeltaTime;
        }   

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _charController.Move(move);
        _animationModule?.SetGround(_isGround == false);
    }

    private void CaclJumpVelocity()
    {
        if(_isJump){
            _jumpVelocity += Time.fixedDeltaTime * _controller.DataSO.Gravity;
            
            if(_jumpVelocity <= 0f){
                _jumpVelocity = 0;
                _isJump = false;
                _animationModule.SetJump(_isJump);    
            }
        }
        else {
            _jumpVelocity += _controller.DataSO.Gravity * Time.fixedDeltaTime;
            _jumpVelocity = Mathf.Clamp(_jumpVelocity, _controller.DataSO.Gravity, 0);
        }
    }
        
    private void SetJump(){
        if(_isGround == false)
            return;

        _isJump = true;
        _jumpVelocity = _controller.DataSO.JumpPower;
        _animationModule.SetJump(_isJump);
    }

    private void SetSprint(bool value){
        if(_isGround){
            if(value){
                _isSprint = _inputVelocity.x == _controller.FrontDir;
            }
            else{
                _isSprint = false;
            }
            _animationModule.SetSprint(_isSprint);
        }
    }

    private void SetMovementVelocity(Vector3 value){
        _animationModule.SetBackward(value.x != _controller.FrontDir);

        _inputVelocity = new Vector3(value.x, 0, 0);
        _movementVelocity = new Vector3(value.x, 0, 0);
    } 

    private void CalcPlayerMovement(){
        _inputVelocity.Normalize();
        
        _animationModule?.SetSpeed(_inputVelocity.sqrMagnitude);

        if(_isSprint == false){
            _movementVelocity *= _controller.DataSO.MoveSpeed * Time.fixedDeltaTime;
        }
        else{
            _movementVelocity *= _controller.DataSO.SprintSpeed * Time.fixedDeltaTime;
        }
    }

    public void StopImmediately(){
        _movementVelocity = Vector3.zero;
        _animationModule?.SetSpeed(0f);
    }

    public override void OnExitModule(){}
}
