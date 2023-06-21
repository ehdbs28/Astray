using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ModuleController
{
    [SerializeField]
    private Transform _playerVisualTrm;
    
    [SerializeField]
    private WeaponController _weapon;
    public WeaponController Weapon => _weapon;

    [SerializeField]
    private LivingDataSO _dataSO;
    public LivingDataSO DataSO => _dataSO;

    private PlayerIKController _ikController;
    public PlayerIKController IkController => _ikController;

    private HealthController _healthController;
    public HealthController HealthController => _healthController;

    public int FrontDir => GetModule<PlayerInputModule>().FrontDir;
    public Vector3 LookDir => GetModule<PlayerInputModule>().UnNormalizeDir;
    public float MouseAngle => GetModule<PlayerInputModule>().MouseAngle;
    public Vector3 AttackDir => GetModule<PlayerInputModule>().NormalizeDir;

    private Coroutine _runningCoroutine = null;

    protected override void Awake()
    {
        base.Awake();

        _ikController = transform.Find("Visual").GetComponent<PlayerIKController>();
        _healthController = GetComponent<HealthController>();
    }

    protected override void Start()
    {
        base.Start();

        GetModule<PlayerInputModule>().OnAttackKeyPress += _weapon.GetModule<WeaponAttackModule>().OnAttackHandle;
        GetModule<PlayerInputModule>().OnReloadKeyPress += _weapon.GetModule<WeaponAttackModule>().Reloading;
    }

    protected override void Update() {
        base.Update();

        SetFrontDir(FrontDir);
        _ikController.SetLookPos(LookDir + transform.position);
        _weapon.GetModule<WeaponAttackModule>().SetAttackDir(AttackDir);
        _weapon.GetModule<WeaponRotateModule>().SetWeaponRotate(MouseAngle);
    }

    public override void Init()
    {
        _healthController.LayerDead(true);
        _weapon.gameObject.SetActive(true);
        _healthController.Init();
    }

    public void StageEnd(){
        GameManager.Instance.StageEnd();
    }

    private void SetFrontDir(int value){
        RotatePlayerDir();
        _ikController.SetHandIK(value);
    }

    private void RotatePlayerDir(){
        if(_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);

        _runningCoroutine = StartCoroutine(PlayerRotate());
        
        Vector3 weaponScale = _weapon.transform.localScale;
        weaponScale.y = 0.05524083f * FrontDir;

        _weapon.transform.localScale = weaponScale;
    }

    private IEnumerator PlayerRotate(){
        float percent = 0f;
        float startAngle = _playerVisualTrm.rotation.eulerAngles.y;
        float endAngle = (FrontDir > 0 ? 120f : 300f);

        while(percent <= 1f){
            percent += Time.deltaTime * _dataSO.RotateSpeed;
            float angle = Mathf.Lerp(startAngle, endAngle, percent);
            _playerVisualTrm.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            yield return null;
        }

        _playerVisualTrm.rotation = Quaternion.AngleAxis(endAngle, Vector3.up);
        _runningCoroutine = null;
    }
}
