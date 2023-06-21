using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : UIScreen
{
    private VisualElement _startBtn;
    private VisualElement _settingBtn;
    private VisualElement _exitBtn;

    public override void RemoveEvent()
    {
    }

    protected override void AddEvent(VisualElement root)
    {
        _startBtn.RegisterCallback<ClickEvent>(e => {
            StageManager.Instance.SetStage();
            UIManager.Instance.ShowPanel(ScreenType.InGame);
        });

        _settingBtn.RegisterCallback<ClickEvent>(e => {
            UIManager.Instance.ShowPanel(PopupType.Setting);
        });

        _exitBtn.RegisterCallback<ClickEvent>(e => {
            Application.Quit();
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _startBtn = root.Q("start-btn");
        _settingBtn = root.Q("setting-btn");
        _exitBtn = root.Q("exit-btn");
    }
}
