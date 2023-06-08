using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ModuleController
{
    [SerializeField]
    private Transform _playerVisualTrm;
    
    [SerializeField]
    private Transform _weaponTrm;

    [SerializeField]
    private LivingDataSO _dataSO;
    public LivingDataSO DataSO => _dataSO;

    private int _frontDir = 1;
    public int FrontDir {
        get{
            return _frontDir;
        }
        
        set{
            _frontDir = value;
            RotatePlayerDir();
        }
    }

    private Coroutine _runningCoroutine = null;

    protected override void Start()
    {
        base.Start();

        GetModule<PlayerInputModule>().OnFrontDirCheck += SetFrontDir;
    }

    private void SetFrontDir(int value){
        if(value == _frontDir)
            return;

        FrontDir = value;
    }

    private void RotatePlayerDir(){
        if(_runningCoroutine != null)
            StopCoroutine(_runningCoroutine);

        _runningCoroutine = StartCoroutine(PlayerRotate());
        
        Vector3 weaponScale = _weaponTrm.localScale;
        weaponScale.y = 0.05524083f * _frontDir;

        _weaponTrm.localScale = weaponScale;
    }

    private IEnumerator PlayerRotate(){
        float percent = 0f;
        float startAngle = _playerVisualTrm.rotation.eulerAngles.y;
        float endAngle = (_frontDir > 0 ? 120f : 300f);

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
