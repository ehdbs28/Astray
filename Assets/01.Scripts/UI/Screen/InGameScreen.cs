using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameScreen : UIScreen
{
    private VisualElement _hpValue;
    private VisualElement _timerValue;
    private VisualElement _bulletValue;

    PlayerController _playerController;

    public override void RemoveEvent()
    {
        _playerController.HealthController.OnDamageEvent -= OnDamageHandle;
        _playerController.GetModule<PlayerAbilityModule>().OnChangePercentEvent -= OnPercentHandle;
        _playerController.Weapon.GetModule<WeaponAttackModule>().OnBulletCountEvent -= OnBulletHandle;
    }

    protected override void AddEvent(VisualElement root)
    {
        _playerController.HealthController.OnDamageEvent += OnDamageHandle;
        _playerController.GetModule<PlayerAbilityModule>().OnChangePercentEvent += OnPercentHandle;
        _playerController.Weapon.GetModule<WeaponAttackModule>().OnBulletCountEvent += OnBulletHandle;
    }

    protected override void FindElement(VisualElement root)
    {
        _hpValue = root.Q("hp-bar").Q("value");
        _timerValue = root.Q("timer-bar").Q("value");
        _bulletValue = root.Q("bullet-bar").Q("value");

        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnDamageHandle(float maxHP, float currentHP){
        float value = Mathf.Lerp(0f, 1f, currentHP / maxHP);
        _hpValue.style.scale = new StyleScale(new Scale(new Vector3(value, 1)));
    }

    private void OnPercentHandle(float percent){
        _timerValue.style.scale = new StyleScale(new Scale(new Vector3(percent, 1)));
    }

    private void OnBulletHandle(int maxBullet, int currentBullet){
        float value = Mathf.Lerp(0f, 1f, (float)currentBullet / (float)maxBullet);
        _bulletValue.style.scale = new StyleScale(new Scale(new Vector3(value, 1)));
    }
}
