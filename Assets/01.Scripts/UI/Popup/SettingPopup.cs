using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingContent{
    private VisualElement _onBtn;
    private VisualElement _offBtn;

    // 오디오 세팅
    public SettingContent(VisualElement root){
        _onBtn = root.Q("on-btn");
        _offBtn = root.Q("off-btn");
        AddEvent();
    }

    private void AddEvent(){
        _onBtn.RegisterCallback<ClickEvent>(e => {
            _offBtn.RemoveFromClassList("on");
            _onBtn.AddToClassList("on");
        });

        _offBtn.RegisterCallback<ClickEvent>(e => {
            _onBtn.RemoveFromClassList("on");
            _offBtn.AddToClassList("on");
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
        _bgm = new SettingContent(root.Q("bgm"));
        _sfx = new SettingContent(root.Q("sfx"));

        _backBtn = root.Q("back-btn");
        _menuBtn = root.Q("menu-btn");
    }

    protected override void AddEvent(VisualElement root)
    {
        _backBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _menuBtn.RegisterCallback<ClickEvent>(e => {
            // 씬 변경도 해 줘야 해
            RemoveRoot();
            UIManager.Instance.ShowPanel(ScreenType.MainMenu);
        });
    }

    public override void RemoveEvent()
    {
    }
}
