using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float _distance;

    [SerializeField]
    private LayerMask _whatIsLayer;

    public override bool MakeADecision()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _distance, _whatIsLayer);
        return cols.Length > 0;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(Selection.activeGameObject == gameObject){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distance);
        }
    }
    #endif
}
