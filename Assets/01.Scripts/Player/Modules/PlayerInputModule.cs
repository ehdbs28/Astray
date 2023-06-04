using static Core.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputModule : CommonModule<PlayerController>
{
    [SerializeField]
    private KeyCode _jumpKey;

    [SerializeField]
    private KeyCode _sprintKey;

    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnJumpKeyPress = null;
    public event Action<bool> OnSprintKeyPress = null;
    public event Action<int> OnFrontDirCheck = null;

    private Vector3 _dirInput;

    public override void OnUpdateModule()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateSprintInput();
        UpdateFrontDir();
    }

    public override void OnDestroyModule()
    {
        OnMovementKeyPress = null;
        OnJumpKeyPress = null;
        OnSprintKeyPress = null;
        OnFrontDirCheck = null;
    }

    private void UpdateJumpInput(){
        if(Input.GetKeyDown(_jumpKey)){
            OnJumpKeyPress?.Invoke();
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
