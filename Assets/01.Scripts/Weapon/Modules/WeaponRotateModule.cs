using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotateModule : CommonModule<WeaponController>
{
    public void SetWeaponRotate(float angle){
        _controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void OnEnterModule(){}
    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
