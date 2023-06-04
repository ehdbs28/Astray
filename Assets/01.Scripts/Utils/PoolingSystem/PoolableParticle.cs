using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;

    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetPositionAndRotation(Vector3 point, Vector3 normal){
        transform.position = point;
        transform.rotation = Quaternion.LookRotation(normal);
    }

    public void Play(){
        StartCoroutine(PlayCallBack());
    }

    private IEnumerator PlayCallBack(){
        _particleSystem.Play();
        yield return new WaitUntil(() => _particleSystem.isPlaying == false);
        PoolManager.Instance.Push(this);
    }

    public override void Init(){
        _particleSystem.Stop();
    }
}
