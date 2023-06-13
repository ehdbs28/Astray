using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIModule : CommonModule<EnemyController>
{
    protected List<AITransition> _transitions;

    public override void SetUp(Transform root){
        base.SetUp(root);

        _transitions = new List<AITransition>();
        transform.GetComponentsInChildren<AITransition>(_transitions);
        _transitions.ForEach(t => t.SetUp(root));
    }

    public override void OnUpdateModule(){
        foreach(var t in _transitions){
            if(t.CheckDecision()){
                _controller.ChangedState(t.NextModule);
                return;
            }
        }
    }

    public abstract override void OnEnterModule();
    public abstract override void OnExitModule();
}
