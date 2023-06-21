using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IDamageable
{
    private ModuleController _mainController;
    private CharacterController _characterController;
    private Animator _mainAnimator;

    [SerializeField]
    private Transform _rig;

    [SerializeField]
    private LivingDataSO _data;

    private List<Collider> _ragDollCols;
    private List<Rigidbody> _limbsRigids;

    private bool _isDie;
    public bool IsDie => _isDie;

    private float _currentHP;

    [SerializeField]
    private UnityEvent OnDieEvent;

    private void Awake() {
        _mainController = GetComponent<ModuleController>();
        _characterController = GetComponent<CharacterController>();
        _mainAnimator = transform.Find("Visual").GetComponent<Animator>();

        _ragDollCols = new List<Collider>();
        _limbsRigids = new List<Rigidbody>();

        _rig.GetComponentsInChildren<Collider>(_ragDollCols);
        _rig.GetComponentsInChildren<Rigidbody>(_limbsRigids);

        RagDollEnable(false);
    
        _currentHP = _data.MaxHP;
    }

    public void OnDamage(float damage, Vector3 point, Vector3 normal)
    {
        _currentHP = Mathf.Clamp(_currentHP - damage, 0, _data.MaxHP);

        if(_currentHP <= 0f){
            _isDie = true;
            OnDieEvent?.Invoke();
        }
    }

    public void RagDollEnable(bool value){
        foreach(var col in _ragDollCols){
            col.enabled = value;
        }

        foreach(var rid in _limbsRigids){
            rid.isKinematic = !value;
        }

        _mainController.enabled = !value;
        _characterController.enabled = !value;
        _mainAnimator.enabled = !value;
    }

    public void LayerDead(bool alive){
        foreach(var col in _ragDollCols){
            col.gameObject.layer = (alive ? 0 : 8);
        }
    }
}
