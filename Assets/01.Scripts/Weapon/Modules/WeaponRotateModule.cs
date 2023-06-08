using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotateModule : CommonModule<WeaponController>
{
    private float _mouseAngle = 0f;

    public override void OnEnterModule(){
        WeaponInputModule inputModule = _controller.GetModule<WeaponInputModule>();

        inputModule.OnMouseAngleCheck += SetMouseAngle;
    }

    public override void OnUpdateModule(){
        _controller.transform.rotation = Quaternion.AngleAxis(_mouseAngle, Vector3.forward);
    }

    private void SetMouseAngle(float angle){
        _mouseAngle = angle;
    }

    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
