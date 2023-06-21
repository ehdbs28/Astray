using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class VCam : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCam;
    public CinemachineBasicMultiChannelPerlin Perlin = null;

    private CameraType _type;
    public CameraType Type => _type;

    public virtual void Awake() {
        VirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public virtual void Setting(CameraType type){
        _type = type;
    }

    public virtual void OnVCam(){
        VirtualCam.Priority = 10;
    }

    public virtual void OffVCam(){
        VirtualCam.Priority = 0;
    }

    public abstract void UpdateVCam();
}
