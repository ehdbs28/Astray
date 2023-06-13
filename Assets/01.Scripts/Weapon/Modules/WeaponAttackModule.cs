using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            StartCoroutine(GunReaction());

            Vector3 firePos = _firePos.position;
            firePos.z = 0f;

            Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
            bullet.Setting(_targetType, _attackDir, _bulletSpeed, _damage);
            bullet.transform.SetPositionAndRotation(firePos, Quaternion.LookRotation(_attackDir));

            PoolableParticle muzzle = PoolManager.Instance.Pop("MuzzleFlashParticle") as PoolableParticle;
            muzzle.SetPositionAndRotation(firePos, Quaternion.LookRotation(_attackDir));
            muzzle.Play();
        }
    }

    public void SetAttackDir(Vector3 dir){
        _attackDir = dir;
    }

    private IEnumerator GunReaction(){
        _controller.transform.position += _attackDir * 0.03f * -1f;
        yield return new WaitForSeconds(_attackDelay / 2);
        _controller.transform.position -= _attackDir * 0.03f * -1f;
    }

    public override void OnEnterModule(){}
    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
