using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAttackModule : CommonModule<WeaponController>
{
    [SerializeField]
    private LivingType _targetType;

    [SerializeField]
    private Transform _firePos;

    [SerializeField]
    private float _attackDelay;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _damage;

    private bool _canAttack = false;
    private float _attackStartTime;

    private Vector3 _attackDir;

    [SerializeField]
    private UnityEvent OnAttackStartEvent;

    public override void OnUpdateModule()
    {
        if(_canAttack == false){
            if(Time.time - _attackStartTime >= _attackDelay){
                _canAttack = true;
            }
        }
    }

    public void OnAttackHandle(){
        if(_canAttack == true){
            _attackStartTime = Time.time;
            _canAttack = false;

            OnAttackStartEvent?.Invoke();

            Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
            bullet.Setting(_targetType, _attackDir, _bulletSpeed, _damage);

            Vector3 firePos = _firePos.position;
            firePos.z = 0f;
            bullet.transform.position = firePos;
            bullet.transform.rotation = Quaternion.LookRotation(_attackDir);
        }
    }

    public void SetAttackDir(Vector3 dir){
        _attackDir = dir;
    }

    public override void OnEnterModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
