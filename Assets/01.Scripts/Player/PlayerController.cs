using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ModuleController
{
    [SerializeField]
    private Transform _weaponTrm;

    [SerializeField]
    private float _rotateSpeed;

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
        // _weaponTrm.rotation = Quaternion.AngleAxis(-30f + (-60f * _frontDir), Vector3.up);
    }

    private IEnumerator PlayerRotate(){
        float percent = 0f;
        float startAngle = transform.rotation.eulerAngles.y;
        float endAngle = 120 + (120f - 120f * _frontDir) / 2;

        while(percent <= 1f){
            percent += Time.deltaTime * _rotateSpeed;
            float angle = Mathf.Lerp(startAngle, endAngle, percent);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            yield return null;
        }

        transform.rotation = Quaternion.AngleAxis(endAngle, Vector3.up);
        _runningCoroutine = null;
    }
}
