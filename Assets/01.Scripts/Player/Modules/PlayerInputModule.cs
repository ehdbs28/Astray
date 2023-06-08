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
    public event Action<int> OnFrontDirCheck = null;
    public event Action OnDodgeKeyPress = null;
    public event Action<Vector3> OnLookAtDirCheck = null;

    [SerializeField]
    private KeyCode _jumpKey;

    [SerializeField]
    private KeyCode _sprintKey;

    [SerializeField]
    private KeyCode _dodgeKey;

    private Vector3 _dirInput;

    public override void OnUpdateModule()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateSprintInput();
        UpdateFrontDir();
        UpdateDodgeInput();
    }

    public override void OnExitModule()
    {
        OnMovementKeyPress = null;
        OnJumpKeyPress = null;
        OnSprintKeyPress = null;
        OnFrontDirCheck = null;
        OnDodgeKeyPress = null;
        OnLookAtDirCheck = null;
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
        screenMousePos.z = Vector3.Distance(transform.position, MainCam.transform.position);
        Vector3 worldMousePos = MainCam.ScreenToWorldPoint(screenMousePos);

        Vector3 dir = worldMousePos - transform.position;
        OnLookAtDirCheck?.Invoke(dir);

        if(worldMousePos.x > transform.position.x){
            OnFrontDirCheck?.Invoke(1);
        }
        else if(worldMousePos.x < transform.position.x){
            OnFrontDirCheck?.Invoke(-1);
        }
    }

    public override void OnEnterModule(){}
    public override void OnFixedUpdateModule(){}
}
