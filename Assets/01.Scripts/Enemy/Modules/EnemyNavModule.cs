using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavModule : CommonModule<EnemyController>
{
    private NavMeshAgent _navMeshAgent;
    private CharacterController _characterController;

    private Vector3 _movementVelocity;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _navMeshAgent = agentRoot.GetComponent<NavMeshAgent>();
        _characterController = agentRoot.GetComponent<CharacterController>();

        _navMeshAgent.speed = _controller.DataSO.MoveSpeed;
    }

    public bool CheckIsArrived(){
        if(_navMeshAgent.pathPending == false && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance){
            return true;
        }
        else{
            return false;
        }
    }

    public void StopImmediately(){
        _navMeshAgent.SetDestination(transform.position);
    }

    public void MoveToTarget(Vector3 pos){
        _navMeshAgent.SetDestination(pos);
    }

    public override void OnEnterModule(){}
    public override void OnExitModule(){}
    public override void OnUpdateModule(){}
    public override void OnFixedUpdateModule(){}
}
