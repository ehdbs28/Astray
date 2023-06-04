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

    public void Awake()
    {
        _animator = transform.GetComponent<Animator>();
        _controller = transform.parent.GetComponent<PlayerController>();
    }

    public void Start()
    {
        _controller.GetModule<PlayerInputModule>().OnFrontDirCheck += SetHandIK;
    }

    private void OnAnimatorIK(int layerIndex) {
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        
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
