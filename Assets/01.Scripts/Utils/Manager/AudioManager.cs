using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioMixer _mixer;

    [SerializeField]
    private AudioClip _bgm, _shot, _reload;

    public bool IsMuteBGM { get; private set; } = false;
    public bool IsMuteSFX { get; private set; } = false;

    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    private void Awake() {
        _clips.Add("BGM", _bgm);
        _clips.Add("Shot", _shot);
        _clips.Add("Reload", _reload);
    }

    public void MixerMute(string name, bool mute){
        _mixer.SetFloat(name, (mute ? -40f : 0f));

        if(name == "BGM"){
            IsMuteBGM = mute;
        }
        else{
            IsMuteSFX = mute;
        }
    }

    public void PlayOneShot(AudioSource source, string clip){
        source.PlayOneShot(_clips[clip]);
    }

    public void PlayBGM(AudioSource source, string clip){
        source.loop = true;
        source.clip = _clips[clip];
        source.Play();
    }
}
