using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;

    private Light _mainLight;

    private void Awake() {
        _mainLight = transform.parent.Find("Directional Light").GetComponent<Light>();
    }

    public void Flash(float dest, float duration){
        GameManager.Instance.RunCoroutine(FlashRoutine(dest, duration));
    }

    private IEnumerator FlashRoutine(float dest, float duration){
        _mainLight.intensity = dest;
        yield return new WaitForSecondsRealtime(duration);
        _mainLight.intensity = 1f;
    }
}
