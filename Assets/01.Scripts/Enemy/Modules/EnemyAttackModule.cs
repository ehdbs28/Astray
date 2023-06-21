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
        _controller.Weapon.GetModule<WeaponAttackModule>().OnAttackHandle();
    }

    public override void OnFixedUpdateModule()
    {
    }
}
