using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationModule : CommonModule<EnemyController>
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    private Animator _animator;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _animator = agentRoot.Find("Visual").GetComponent<Animator>();
    }

    public void SetSpeed(float speed){
        _animator.SetFloat(_speedHash, speed);
    }

    public override void OnEnterModule(){}
    public override void OnExitModule(){}
    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
}
