using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserDamage = 15f;

    private void Awake() {
        for(int i = 0; i < transform.childCount; i++){
            Transform trm = transform.GetChild(i);
            LineRenderer lineRenderer = trm.Find("Beam").GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, trm.position);
            lineRenderer.SetPosition(1, trm.position + Vector3.down * 4.5f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
            damageable.OnDamage(_laserDamage, Vector3.zero, Vector3.zero);
        }
    }
}
