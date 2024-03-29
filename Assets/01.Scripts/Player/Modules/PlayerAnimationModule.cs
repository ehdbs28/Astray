using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationModule : CommonModule<PlayerController>
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    private readonly int _isAirboneHash = Animator.StringToHash("is_airbone");

    private readonly int _jumpTriggerHash = Animator.StringToHash("jump");
    private readonly int _isJumpHash = Animator.StringToHash("is_jump");

    private readonly int _isSprintHash = Animator.StringToHash("is_sprint");

    private readonly int _isBackwardHash = Animator.StringToHash("is_backward");

    private readonly int _dodgeTriggerHash = Animator.StringToHash("dodge");
    private readonly int _isDodgeHash = Animator.StringToHash("is_dodge");

    private readonly int _backFlipTriggerHash = Animator.StringToHash("backflip");
    private readonly int _isBackFliphash = Animator.StringToHash("is_backflip");

    private Animator _animator;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _animator = agentRoot.Find("Visual").GetComponent<Animator>();
    }

    public void SetSpeed(float value){
        _animator.SetFloat(_speedHash, value);
    }

    public void SetGround(bool value){
        _animator.SetBool(_isAirboneHash, value);
    }

    public void SetJump(bool value){
        _animator.SetBool(_isJumpHash, value);
        if(value){
            _animator.SetTrigger(_jumpTriggerHash);
        }   
        else{
            _animator.ResetTrigger(_jumpTriggerHash);
        }
    }

    public void SetBackFlip(bool value){
        _animator.SetBool(_isBackFliphash, value);
        if(value){
            _animator.SetTrigger(_backFlipTriggerHash);
        }
        else{
            _animator.ResetTrigger(_backFlipTriggerHash);
        }
    }

    public void SetDodge(bool value){
        _animator.SetBool(_isDodgeHash, value);
        if(value){
            _animator.SetTrigger(_dodgeTriggerHash);
        }
        else{
            _animator.ResetTrigger(_dodgeTriggerHash);
        }
    }

    public void SetSprint(bool value){
        _animator.SetBool(_isSprintHash, value);
    }

    public void SetBackward(bool value){
        _animator.SetBool(_isBackwardHash, value);
    }

    public override void OnEnterModule(){}
    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
