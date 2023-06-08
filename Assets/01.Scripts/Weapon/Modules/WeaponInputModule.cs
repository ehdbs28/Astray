using static Core.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInputModule : CommonModule<WeaponController>
{
    public event Action<float> OnMouseAngleCheck = null;
    public event Action OnAttackKeyPress = null;
    public event Action<Vector3> OnAttackDirCheck = null;

    public override void OnUpdateModule(){
        UpdateMouseAngle();
        UpdateAttackInput();
    }

    public override void OnExitModule(){
        OnMouseAngleCheck = null;
        OnAttackKeyPress = null;
    }

    private void UpdateAttackInput(){
        if(Input.GetMouseButton(0)){
            OnAttackKeyPress?.Invoke();
        }
    }

    private void UpdateMouseAngle(){
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Vector3.Distance(transform.position, MainCam.transform.position);
        Vector3 worldMousePos = MainCam.ScreenToWorldPoint(screenMousePos);

        Vector3 dir = (worldMousePos - transform.position).normalized;
        OnAttackDirCheck?.Invoke(dir);

        float cos = Vector3.Dot(Vector3.right, dir);
        float theta = Mathf.Acos(cos) * Mathf.Rad2Deg;

        if(worldMousePos.y <= transform.position.y){
            theta *= -1f;
        }

        OnMouseAngleCheck?.Invoke(theta);
    }

    public override void OnEnterModule(){}
    public override void OnFixedUpdateModule(){}
}
