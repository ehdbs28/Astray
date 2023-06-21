using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;

    private Volume _volume;

    [SerializeField]
    private VolumeProfile _mainProfile;

    [SerializeField]
    private VolumeProfile _slowDownProfile;

    private readonly Dictionary<string, VolumeProfile> _profiles = new Dictionary<string, VolumeProfile>();

    private void Awake() {
        _volume = transform.parent.Find("Global Volume").GetComponent<Volume>();

        _profiles.Add("MainProfile", _mainProfile);
        _profiles.Add("SlowDownProfile", _slowDownProfile);
    }

    public void SetProfile(string profile){
        if(_profiles.ContainsKey(profile) == false){
            Debug.LogError($"Doesn't exist profile in diction : {profile}");
            return;
        }

        _volume.profile = _profiles[profile];
    }
}
