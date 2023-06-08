using static Core.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputModule : CommonModule<PlayerController>
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnJumpKeyPress = null;
    public event Action<bool> OnSprintKeyPress = null;
    public event Action OnDodgeKeyPress = null;
    public event Action OnAttackKeyPress = null;

    [SerializeField]
    private KeyCode _jumpKey;

    [SerializeField]
    private KeyCode _sprintKey;

    [SerializeField]
    private KeyCode _dodgeKey;

    private Vector3 _dirInput;

    private int _frontDir;
    private float _mouseAngle;
    private Vector3 _unNormalizeDir;
    private Vector3 _normalizeDir;

    public int FrontDir => _frontDir;
    public float MouseAngle => _mouseAngle;
    public Vector3 UnNormalizeDir => _unNormalizeDir;
    public Vector3 NormalizeDir => _normalizeDir;

    public override void OnUpdateModule()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateSprintInput();
        UpdateFrontDir();
        UpdateDodgeInput();
        UpdateAttackInput();
    }

    public override void OnExitModule()
    {
        OnMovementKeyPress = null;
        OnJumpKeyPress = null;
        OnSprintKeyPress = null;
        OnDodgeKeyPress = null;
        OnAttackKeyPress = null;
    }

    private void UpdateJumpInput(){
        if(Input.GetKeyDown(_jumpKey)){
            OnJumpKeyPress?.Invoke();
        }
    }

    private void UpdateDodgeInput(){
        if(Input.GetKeyDown(_dodgeKey)){
            OnDodgeKeyPress?.Invoke();
        }
    }

    private void UpdateAttackInput(){
        if(Input.GetMouseButton(0)){
            OnAttackKeyPress?.Invoke();
        }
    }

    private void UpdateSprintInput(){
        if(Input.GetKeyDown(_sprintKey) || Input.GetKey(_sprintKey)){
            OnSprintKeyPress?.Invoke(true);
        }
        else{
            OnSprintKeyPress?.Invoke(false);
        }
    }

    private void UpdateMovementInput(){
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _dirInput = new Vector3(h, v);
        OnMovementKeyPress?.Invoke(_dirInput);
    }

    private void UpdateFrontDir(){
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Vector3.Distance(_controller.Weapon.transform.position, MainCam.transform.position);
        Vector3 worldMousePos = MainCam.ScreenToWorldPoint(screenMousePos);

        Vector3 dir = worldMousePos - _controller.Weapon.transform.position;

        _unNormalizeDir = dir;
        _normalizeDir = dir.normalized;

        if(worldMousePos.x > _controller.Weapon.transform.position.x){
            _frontDir = 1;
        }
        else if(worldMousePos.x < _controller.Weapon.transform.position.x){
            _frontDir = -1;
        }

        float cos = Vector3.Dot(Vector3.right, _normalizeDir);
        float theta = Mathf.Acos(cos) * Mathf.Rad2Deg;

        if(worldMousePos.y <= _controller.Weapon.transform.position.y){
            theta *= -1f;
        }

        _mouseAngle = theta;
    }

    public override void OnEnterModule(){}
    public override void OnFixedUpdateModule(){}
}
