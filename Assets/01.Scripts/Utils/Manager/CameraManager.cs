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

    public void ShakeCam(float intensity, float duration){
        if(_currentVCam.Perlin == null)
            return;

        StopAllCoroutines();
        StartCoroutine(ShakeCamCoroutine(intensity, duration));
    }

    IEnumerator ShakeCamCoroutine(float intensity, float endTime){
        _currentVCam.Perlin.m_AmplitudeGain = intensity;

        float currentTime = 0f;
        while(currentTime < endTime){
            yield return null;
            
            if (_currentVCam.Perlin == null) 
                break;

            _currentVCam.Perlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0, currentTime / endTime);
            currentTime += Time.deltaTime;
        }

        if(_currentVCam.Perlin != null)
            _currentVCam.Perlin.m_AmplitudeGain = 0;
    }
} 
