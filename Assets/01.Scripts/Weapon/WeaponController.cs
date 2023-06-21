using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : ModuleController
{
    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
}
