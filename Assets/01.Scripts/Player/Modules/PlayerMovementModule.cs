using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovementModule : CommonModule<PlayerController>
{
    private CharacterController _charController;
    public CharacterController CharController => _charController;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;

    private float _verticalVelocity;
    [SerializeField]
    private float _jumpVelocity;

    private Vector3 _inputVelocity;

    private bool _isSprint = false;
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isWallRun = false;
    
    [SerializeField]
    private LayerMask _whatIsWall;

    [SerializeField]
    private Transform _leftRayPos, _rightRayPos;

    [SerializeField]
    private float _rayDis;

    [SerializeField]
    private Transform _rightFoot;

    [SerializeField]
    private Transform _leftFoot;

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
        if(_charController.isGrounded == false && _isGround == true && _isJump == false){
            _jumpVelocity = _controller.DataSO.Gravity * Time.fixedDeltaTime;
        }
        _isGround = _charController.isGrounded;
        _isWallRun = IsWallCheck();
    }

    public override void OnFixedUpdateModule()
    {
        CalcPlayerMovement();
        CaclJumpVelocity();

        if(_isGround == false){
            _verticalVelocity = _jumpVelocity * Time.fixedDeltaTime;
        }
        else{
            _animationModule.SetBackFlip(false);    
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

    private bool IsWallCheck(){
        Ray rightRay = new Ray(_rightRayPos.position, Vector3.right * _controller.FrontDir);
        Ray leftRay = new Ray(_leftRayPos.position, Vector3.left * _controller.FrontDir);

        bool isHitRight = Physics.Raycast(rightRay, _rayDis,_whatIsWall);
        bool isHitLeft = Physics.Raycast(leftRay, _rayDis,_whatIsWall);

        return (isHitRight || isHitLeft) && !_isGround;
    }
        
    private void SetJump(){
        if(_isGround == false && _isWallRun == false || _isJump)
            return;

        _isJump = true;

        if(_isWallRun){
            _jumpVelocity = _controller.DataSO.WallJumpPower;
            _animationModule.SetBackFlip(_isJump);
        }
        else{
            _jumpVelocity = _controller.DataSO.JumpPower * (_isSprint ? 1.25f : 1f);
            _animationModule.SetJump(_isJump);
        }
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
