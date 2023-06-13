using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseModule : AIModule
{
    public override void OnEnterModule(){
        _controller.GetModule<EnemyAnimationModule>().SetSpeed(0.2f);
    }

    public override void OnExitModule(){
        _controller.GetModule<EnemyNavModule>().StopImmediately();
        _controller.GetModule<EnemyAnimationModule>().SetSpeed(0f);
    }

    public override void OnUpdateModule()
    {
        _controller.GetModule<EnemyNavModule>().MoveToTarget(_controller.ActionData.Player.transform.position);
        base.OnUpdateModule();
    }

    public override void OnFixedUpdateModule(){}
}
