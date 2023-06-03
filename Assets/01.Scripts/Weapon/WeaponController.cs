using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : ModuleController
{
    private PlayerController _playerController;
    public PlayerController PlayerController => _playerController;

    protected override void Awake()
    {
        base.Awake();

        _playerController = transform.parent.GetComponent<PlayerController>();
    }
}
