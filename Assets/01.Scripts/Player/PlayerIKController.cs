using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandIKTarget{
    public Transform RightHandTarget;
    public Transform LeftHandTarget;
}

public class PlayerIKController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _controller;

    [SerializeField]
    private HandIKTarget _rightDirTarget;

    [SerializeField]
    private HandIKTarget _leftDirTarget;

    private HandIKTarget _currentHandIK = null;

    private int _frontDir => _controller.GetModule<PlayerInputModule>().FrontDir;
    private Vector3 _lookAtPos;

    public void Awake()
    {
        _animator = transform.GetComponent<Animator>();
        _controller = transform.parent.GetComponent<PlayerController>();
    }

    private void Update() {
        SetHandIK(_frontDir);
        _lookAtPos = _controller.LookAtDir + transform.position;
    }

    private void OnAnimatorIK(int layerIndex) {
        if(!_animator) return;

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(_lookAtPos);
        
        if(_currentHandIK != null){
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _currentHandIK.RightHandTarget.position);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _currentHandIK.LeftHandTarget.position);

            _animator.SetIKRotation(AvatarIKGoal.RightHand, _currentHandIK.RightHandTarget.rotation);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _currentHandIK.LeftHandTarget.rotation);
        }
    }

    private void SetHandIK(int value){
        _currentHandIK = (value > 0 ? _rightDirTarget : _leftDirTarget);
    }
}
