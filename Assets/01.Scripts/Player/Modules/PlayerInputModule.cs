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

    private Vector3 _dirInput;

    public override void OnEnterModule()
    {
    }

    public override void OnUpdateModule()
    {
        UpdateMovementInput();
        UpdateJumpInput();
        UpdateSprintInput();
    }

    private void UpdateJumpInput(){
        if(Input.GetKeyDown(_jumpKey)){
            OnJumpKeyPress?.Invoke();
        }
    }

    private void UpdateSprintInput(){
        if(Input.GetKeyDown(_sprintKey)){
            OnSprintKeyPress?.Invoke(true);
        }
        else if(Input.GetKeyUp(_sprintKey)){
            OnSprintKeyPress?.Invoke(false);
        }
    }

    private void UpdateMovementInput(){
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _dirInput = new Vector3(h, v);
        OnMovementKeyPress?.Invoke(_dirInput);
    }

    public override void OnDestroyModule()
    {
        OnMovementKeyPress = null;
    }

    public override void OnFixedUpdateModule(){}
}
