using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : ModuleController
{
    [SerializeField]
    private float _deleteDelay = 5f;

    private Rigidbody _rigid;
    private Collider _collider;

    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _collider.enabled = false;
        _rigid.isKinematic = true;
    }

    public void DropWeapon(){
        transform.SetParent(null);
        _collider.enabled = true;
        _rigid.isKinematic = false;
        StartCoroutine(Remove());
    }

    private IEnumerator Remove(){
        yield return new WaitForSeconds(_deleteDelay);
        Destroy(gameObject);
    }
}
