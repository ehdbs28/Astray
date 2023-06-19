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

    [SerializeField]
    private Transform _head;
    public Transform Head => _head;

    [SerializeField]
    private HandIKTarget _rightDirTarget;

    [SerializeField]
    private HandIKTarget _leftDirTarget;

    private HandIKTarget _currentHandIK = null;

    private Vector3 _lookPos;

    public void Awake(){
        _animator = transform.GetComponent<Animator>();
    }

    public void SetLookPos(Vector3 lookPos){
        _lookPos = lookPos;
    }

    private void OnAnimatorIK(int layerIndex) {
        if(!_animator) return;

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(_lookPos);
        
        if(_currentHandIK != null){
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _currentHandIK.RightHandTarget.position);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _currentHandIK.LeftHandTarget.position);

            _animator.SetIKRotation(AvatarIKGoal.RightHand, _currentHandIK.RightHandTarget.rotation);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _currentHandIK.LeftHandTarget.rotation);
        }
    }

    public void SetHandIK(int value){
        _currentHandIK = (value > 0 ? _rightDirTarget : _leftDirTarget);
    }
}
