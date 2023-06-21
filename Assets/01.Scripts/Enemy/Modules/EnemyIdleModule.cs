using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyIdleModule : AIModule
{
    public override void OnEnterModule(){
        _controller.ActionData.IsDetection = false;
    }

    public override void OnExitModule(){
    }

    public override void OnFixedUpdateModule()
    {
    }
}
