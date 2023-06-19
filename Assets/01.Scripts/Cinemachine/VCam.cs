using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class VCam : MonoBehaviour
{
    protected CinemachineVirtualCamera _vCam;

    private CameraType _type;
    public CameraType Type => _type;

    private void Awake() {
        _vCam = GetComponent<CinemachineVirtualCamera>();
    }

    public virtual void Setting(CameraType type){
        _type = type;
    }

    public virtual void OnVCam(){
        _vCam.Priority = 10;
    }

    public virtual void OffVCam(){
        _vCam.Priority = 0;
    }

    public abstract void UpdateVCam();
}
