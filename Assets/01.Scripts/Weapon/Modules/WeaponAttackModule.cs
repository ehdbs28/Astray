using System;
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

    [SerializeField]
    private float _reloadDelay = 0.5f;

    private bool _canAttack = false;
    private bool _isReloading = false;

    private float _attackStartTime;

    private Vector3 _attackDir;

    private const int _maxBullet = 30;
    private int _currentBullet = 0;

    public Action<int, int> OnBulletCountEvent = null;

    public override void OnEnterModule(){
        _currentBullet = _maxBullet;
    }

    public override void OnUpdateModule()
    {
        if(_canAttack == false){
            if(Time.time - _attackStartTime >= _attackDelay){
                _canAttack = true;
            }
        }
    }

    public void OnAttackHandle(){
        if(_canAttack == true  && _isReloading == false){
            _attackStartTime = Time.time;
            _canAttack = false;
            _currentBullet = Mathf.Clamp(_currentBullet - 1, 0, _maxBullet);
            OnBulletCountEvent?.Invoke(_maxBullet, _currentBullet);

            if(_currentBullet <= 0){
                StartCoroutine(Reload());
                return;
            }

            StartCoroutine(GunReaction());

            if(_targetType == LivingType.ENEMY){
                CameraManager.Instance.ShakeCam(0.75f, _attackDelay / 2);
            }

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

    public void Reloading(){
        if(_isReloading == false)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload(){
        _isReloading = true;
        yield return new WaitForSeconds(_reloadDelay);
        _currentBullet = _maxBullet;
        _isReloading = false;
        OnBulletCountEvent?.Invoke(_maxBullet, _currentBullet);
    }

    private IEnumerator GunReaction(){
        _controller.transform.position += _attackDir * 0.03f * -1f;
        yield return new WaitForSeconds(_attackDelay / 2);
        _controller.transform.position -= _attackDir * 0.03f * -1f;
    }

    public override void OnFixedUpdateModule(){}
    public override void OnExitModule(){}
}
