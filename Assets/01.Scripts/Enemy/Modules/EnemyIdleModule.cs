using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyIdleModule : AIModule
{
    [SerializeField]
    private float _detectionRadius;

    [SerializeField]
    private LayerMask _targetLayer;

    public override void OnEnterModule(){
    }

    public override void OnExitModule(){
    }

    public override void OnUpdateModule()
    {
        base.OnUpdateModule();
        TargetCheck();
    }

    private void TargetCheck(){
        Collider[] cols = Physics.OverlapSphere(transform.position, _detectionRadius, _targetLayer);
        if(cols.Length > 0){
            _controller.ActionData.TargetSpotted = true;
            _controller.ActionData.Target = cols[0].transform;
        }
    }

    public override void OnFixedUpdateModule(){}

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(Selection.activeGameObject == gameObject){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
    #endif
}
