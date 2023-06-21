using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFollowVCam : VCam
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _rotateSpeed = 5f;

    public override void Awake()
    {
        base.Awake();
        Perlin = VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void UpdateVCam()
    {
        Vector3 dir = GetMouseDir();
        Quaternion dest = Quaternion.Euler(dir.y * -10f, dir.x * 10f, 0);

        Quaternion lerp = Quaternion.Lerp(VirtualCam.transform.rotation, dest, Time.deltaTime * _rotateSpeed);

        VirtualCam.transform.rotation = lerp;
    }

    public void SetTarget(Transform target){
        _target = target;

        VirtualCam.m_Follow = _target;
    }

    private Vector3 GetMouseDir(){
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.x -= 1920 / 2;
        screenMousePos.y -= 1080 / 2;
        
        float dot = screenMousePos.x * screenMousePos.x + screenMousePos.y * screenMousePos.y;
        float dis = Mathf.Sqrt(dot);

        Vector3 normalize = new Vector3(screenMousePos.x / dis, screenMousePos.y / dis);

        return normalize;
    }
}
