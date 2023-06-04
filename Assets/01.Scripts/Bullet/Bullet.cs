using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    private LivingType _targetType;

    private Rigidbody _rigid;

    private Vector3 _dir;
    private float _speed;
    private float _damage;

    private Vector3 _shotVelocity;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        ShotBullet();        
    }

    private void OnCollisionEnter(Collision other) {
        StopCoroutine("DestroyCoroutine");

        if(other.collider.CompareTag("Living")){
            HitLivingCreature(other);
        }
    }

    public void Setting(LivingType targetType, Vector3 dir, float speed, float damage){
        _targetType = targetType;
        _dir = dir;
        _speed = speed;
        _damage = damage;

        StartCoroutine("DestroyCoroutine");
    }

    private void ShotBullet(){
        _rigid.velocity = _dir * _speed * Time.fixedDeltaTime;
    }

    private void HitLivingCreature(Collision other){
        if(other.transform.TryGetComponent<IDamageable>(out IDamageable damageable)){
            Vector3 point = other.contacts[0].point;
            Vector3 normal = other.contacts[0].normal;

            damageable.OnDamage(_damage, point, normal);
        }
    }

    private IEnumerator DestroyCoroutine(){
        yield return new WaitForSeconds(10f);
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        _rigid.velocity = Vector3.zero;
    }
}
