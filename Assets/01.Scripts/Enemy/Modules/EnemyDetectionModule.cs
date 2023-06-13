using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionModule : AIModule
{
    [SerializeField]
    private float _ditectionTime = 3f;

    [SerializeField]
    private float _ditectionRange = 7f;

    [SerializeField]
    private LayerMask _whatIsTarget;

    private float _startTime;
    private float _currentTime;

    public override void OnEnterModule()
    {
        _startTime = Time.time;
        _currentTime = _startTime;
    }

    public override void OnExitModule()
    {
    }

    public override void OnUpdateModule()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime - _startTime >= _ditectionTime){
            Collider[] cols = Physics.OverlapSphere(transform.position, _ditectionRange, _whatIsTarget);
            if(cols.Length > 0)
                _controller.ActionData.Player = cols[0].GetComponent<PlayerController>();
        }

        base.OnUpdateModule();
    }

    public override void OnFixedUpdateModule()
    {
    }
}
