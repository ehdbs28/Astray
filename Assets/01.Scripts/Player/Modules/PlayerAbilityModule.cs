using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityModule : CommonModule<PlayerController>
{
    [SerializeField][Range(0f, 1f)]
    private float _timeSlowValue = 0.45f;

    private bool _isTimeSlow;
    public bool IsTimeSlow => _isTimeSlow;

    public override void OnEnterModule(){
        _controller.GetModule<PlayerInputModule>().OnAbilityKeyPress += AbilityHandle;
    }

    private void AbilityHandle(bool input){
        if(input){
            LightManager.Instance.Flash(5f, 0.05f);
            VolumeManager.Instance.SetProfile("SlowDownProfile");
            TimeManager.Instance.TimeScale = _timeSlowValue;
        }
        else{
            LightManager.Instance.Flash(2.5f, 0.05f);
            VolumeManager.Instance.SetProfile("MainProfile");
            TimeManager.Instance.TimeScale = 1f;
        }
    }

    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
