using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ModuleController
{
    [SerializeField]
    private Transform _visualTrm;
    
    [SerializeField]
    private WeaponController _weapon;
    public WeaponController Weapon => _weapon;

    [SerializeField]
    private AIModule _currentModule;
    public AIModule CurrentModule => _currentModule;

    [SerializeField]
    private LivingDataSO _dataSO;
    public LivingDataSO DataSO => _dataSO;

    [SerializeField]
    private float _destroyDelay = 5f;

    private EnemyActionData _actionData;
    public EnemyActionData ActionData => _actionData;

    private PlayerIKController _ikController;

    private int _frontDir;

    private Coroutine _runningCoroutine = null;

    protected override void Awake()
    {
        _actionData = transform.GetComponent<EnemyActionData>();
        _ikController = transform.Find("Visual").GetComponent<PlayerIKController>();
        base.Awake();
    }

    protected override void Start(){
        ChangedState(_currentModule);
    }

    protected override void Update(){
        _currentModule?.OnUpdateModule();

        if(_actionData.Player){
            SetFrontDir();
            SetLookPos();
        }
    }

    protected override void FixedUpdate(){
        _currentModule?.OnFixedUpdateModule();
    }

    private void SetFrontDir(){
        Vector3 dir = (_actionData.Player.transform.position - transform.position).normalized;
        _frontDir = dir.x > 0 ? 1 : -1;
        RotateDir();
        _ikController.SetHandIK(_frontDir);
    }

    private void SetLookPos(){
        Vector3 lookPos = _actionData.Player.transform.Find("Visual").GetComponent<PlayerIKController>().Head.position;
        _ikController.SetLookPos(lookPos);
    }

    private void RotateDir(){
        if(_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);

        _runningCoroutine = StartCoroutine(Rotate());

        Vector3 weaponScale = _weapon.transform.localScale;
        weaponScale.y = 0.05524086f * _frontDir;

        _weapon.transform.localScale = weaponScale;
    }

    private IEnumerator Rotate(){
        float percent = 0f;
        float startAngle = _visualTrm.rotation.eulerAngles.y;
        float endAngle = (_frontDir > 0 ? 120f : 300f);

        while(percent <= 1f){
            percent += Time.deltaTime * _dataSO.RotateSpeed;
            float angle = Mathf.Lerp(startAngle, endAngle, percent);
            _visualTrm.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            yield return null;
        }

        _visualTrm.rotation = Quaternion.AngleAxis(endAngle, Vector3.up);
        _runningCoroutine = null;
    }

    public void ChangedState(AIModule next){
        _currentModule?.OnExitModule();
        _currentModule = next;
        _currentModule.OnEnterModule();
    }

    public void OnDieHandle(){
        StartCoroutine(OnDie());
    }

    private IEnumerator OnDie(){
        yield return new WaitForSeconds(_destroyDelay);
        PoolManager.Instance.Push(this);
    }
}
