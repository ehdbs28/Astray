using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;
    private float _waitTime;

    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
        _waitTime = _particleSystem.main.duration + 0.1f;
    }

    public void SetPositionAndRotation(Vector3 point, Vector3 normal){
        transform.SetLocalPositionAndRotation(point, Quaternion.LookRotation(normal));
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot){
        transform.SetPositionAndRotation(pos, rot);
    }

    public void Play(){
        StartCoroutine(PlayCallBack());
    }

    private IEnumerator PlayCallBack(){
        _particleSystem.Play();
        yield return new WaitForSeconds(_waitTime);
        PoolManager.Instance.Push(this);
    }

    public override void Init(){
        _particleSystem.Stop();
    }
}
