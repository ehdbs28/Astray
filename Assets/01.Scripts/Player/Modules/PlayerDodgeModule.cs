using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeModule : CommonModule<PlayerController>
{
    [SerializeField]
    private float _dodgeTime;

    private bool _isDodge;

    private PlayerAnimationModule _animationModule => _controller.GetModule<PlayerAnimationModule>();

    public override void OnEnterModule()
    {
        _controller.GetModule<PlayerInputModule>().OnDodgeKeyPress += OnDodgeHandle;
    }

    private void OnDodgeHandle(){
        StopCoroutine("DodgeCoroutine");
        _isDodge = true;
        _animationModule.SetDodge(_isDodge);
        StartCoroutine("DodgeCoroutine");
    }

    private IEnumerator DodgeCoroutine(){
        yield return new WaitForSeconds(_dodgeTime);
        _isDodge = false;
        _animationModule.SetDodge(_isDodge);
    }
    
    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
