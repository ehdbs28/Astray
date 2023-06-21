using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField]
    private List<VCam> _vCams = new List<VCam>();

    private VCam _currentVCam = null;

    public void Awake() {
        for(int i = 0; i < _vCams.Count; i++){
            _vCams[i].Setting((CameraType)i);
        }
    }

    private void Update() {
        _currentVCam?.UpdateVCam();
    }

    public VCam SetVCam(CameraType next){
        VCam virtualCam = null;
        
        foreach(VCam cam in _vCams.Where(cam => cam.Type == next)){
            virtualCam = cam;
        }

        if(virtualCam == null)
            return null;

        _currentVCam?.OffVCam();
        _currentVCam = virtualCam;
        _currentVCam.OnVCam();

        return virtualCam;
    }
} 
