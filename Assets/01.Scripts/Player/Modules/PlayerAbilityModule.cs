using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityModule : CommonModule<PlayerController>
{
    [SerializeField][Range(0f, 1f)]
    private float _timeSlowValue = 0.45f;

    private bool _isTimeSlow;
    public bool IsTimeSlow => _isTimeSlow;

    private float _percent = 1f;

    public Action<float> OnChangePercentEvent = null;

    public override void OnEnterModule(){
        _isTimeSlow = false;
        _controller.GetModule<PlayerInputModule>().OnAbilityKeyPress += AbilityHandle;
    }

    public override void OnUpdateModule(){
        if(_isTimeSlow){
            _percent -= Time.deltaTime * 0.5f;
            _percent = Mathf.Clamp(_percent, 0f, 1f);
            if(_percent <= 0f)
                SlowDown(false);
        }
        else{
            _percent += Time.deltaTime * 0.5f;
            _percent = Mathf.Clamp(_percent, 0f, 1f);
        }

        OnChangePercentEvent?.Invoke(_percent);
    }

    private void AbilityHandle(bool input){
        if(_percent > 0f)
            SlowDown(input);
    }

    private void SlowDown(bool value){
        if(value){
            _isTimeSlow = true;
            LightManager.Instance.Flash(5f, 0.05f);
            VolumeManager.Instance.SetProfile("SlowDownProfile");
            TimeManager.Instance.TimeScale = _timeSlowValue;
        }
        else{
            _isTimeSlow = false;
            LightManager.Instance.Flash(2.5f, 0.05f);
            VolumeManager.Instance.SetProfile("MainProfile");
            TimeManager.Instance.TimeScale = 1f;
        }
    }

    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
