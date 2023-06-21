using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingContent{
    private VisualElement _onBtn;
    private VisualElement _offBtn;

    private string _name;

    // 오디오 세팅
    public SettingContent(VisualElement root, string name){
        _onBtn = root.Q("on-btn");
        _offBtn = root.Q("off-btn");
        _name = name;

        if(_name == "BGM"){
            if(AudioManager.Instance.IsMuteBGM)
                _offBtn.AddToClassList("on");
            else
                _onBtn.AddToClassList("on");
        }
        else{
            if(AudioManager.Instance.IsMuteSFX)
                _offBtn.AddToClassList("on");
            else
                _onBtn.AddToClassList("on");
        }

        AddEvent();
    }

    private void AddEvent(){
        _onBtn.RegisterCallback<ClickEvent>(e => {
            _offBtn.RemoveFromClassList("on");
            _onBtn.AddToClassList("on");
            AudioManager.Instance.MixerMute(_name, false);
        });

        _offBtn.RegisterCallback<ClickEvent>(e => {
            _onBtn.RemoveFromClassList("on");
            _offBtn.AddToClassList("on");
            AudioManager.Instance.MixerMute(_name, true);
        });
    }
}

public class SettingPopup : UIPopup
{
    private SettingContent _bgm;
    private SettingContent _sfx;

    private VisualElement _backBtn;
    private VisualElement _menuBtn;

    protected override void FindElement(VisualElement root)
    {
        _bgm = new SettingContent(root.Q("bgm"), "BGM");
        _sfx = new SettingContent(root.Q("sfx"), "SFX");

        _backBtn = root.Q("back-btn");
        _menuBtn = root.Q("menu-btn");
    }

    protected override void AddEvent(VisualElement root)
    {
        _backBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _menuBtn.RegisterCallback<ClickEvent>(e => {
            StageManager.Instance.ExitStage();
            RemoveRoot();
            UIManager.Instance.ShowPanel(ScreenType.MainMenu);
        });
    }

    public override void RemoveEvent()
    {
    }
}
