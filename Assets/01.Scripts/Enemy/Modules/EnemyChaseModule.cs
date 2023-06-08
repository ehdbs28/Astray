using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseModule : AIModule
{
    public override void OnEnterModule(){
        _controller.GetModule<EnemyAnimationModule>().SetSpeed(0.2f);
    }

    public override void OnExitModule(){
        _controller.GetModule<EnemyAnimationModule>().SetSpeed(0f);
    }

    public override void OnUpdateModule()
    {
        base.OnUpdateModule();

        _controller.GetModule<EnemyNavModule>().MoveToTarget(_controller.ActionData.Target.position);
    }

    public override void OnFixedUpdateModule(){}
}
