using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _muzzleFlash;

    public void PlayMuzzleFlash(){
        _muzzleFlash.Play();
    }
}
