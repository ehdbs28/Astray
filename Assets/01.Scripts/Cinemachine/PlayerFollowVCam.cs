using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowVCam : VCam
{
    [SerializeField]
    private Transform _target;


    public override void UpdateVCam()
    {
    }

    public void SetTarget(Transform target){
        _target = target;

        _vCam.m_Follow = _target;
    }
}
