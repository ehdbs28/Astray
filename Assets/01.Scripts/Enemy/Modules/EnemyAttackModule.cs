using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackModule : AIModule
{
    public override void OnEnterModule()
    {
    }

    public override void OnExitModule()
    {
    }

    public override void OnUpdateModule()
    {
        base.OnUpdateModule();
        Attack();
    }

    private void Attack(){
        Vector3 attackDir = (_controller.ActionData.Player.transform.position
                            - transform.position).normalized;

        float cos = Vector3.Dot(Vector3.right, attackDir);
        float theta = Mathf.Acos(cos) * Mathf.Rad2Deg;
        
        _controller.Weapon.GetModule<WeaponRotateModule>().SetWeaponRotate(theta);
        _controller.Weapon.GetModule<WeaponAttackModule>().SetAttackDir(attackDir);

        _controller.Weapon.GetModule<WeaponAttackModule>().OnAttackHandle();
    }

    public override void OnFixedUpdateModule()
    {
    }
}
