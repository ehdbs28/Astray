using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ModuleController
{
    [SerializeField]
    private AIModule _currentModule;
    public AIModule CurrentModule => _currentModule;

    [SerializeField]
    private LivingDataSO _dataSO;
    public LivingDataSO DataSO => _dataSO;

    private EnemyActionData _actionData;
    public EnemyActionData ActionData => _actionData;

    protected override void Awake()
    {
        _actionData = transform.GetComponent<EnemyActionData>();
        base.Awake();
    }

    protected override void Start(){
        ChangedState(_currentModule);
    }

    protected override void Update(){
        _currentModule?.OnUpdateModule();
    }

    protected override void FixedUpdate(){
        _currentModule?.OnFixedUpdateModule();
    }

    public void ChangedState(AIModule next){
        _currentModule?.OnExitModule();
        _currentModule = next;
        _currentModule.OnEnterModule();
    }
}
