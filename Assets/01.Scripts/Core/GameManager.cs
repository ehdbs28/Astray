using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _poolingTrm;

    [SerializeField]
    private AudioSource _masterSource;

    private void Awake() {
        if(Instance != null){
            Debug.LogError("Multiple GameManager is running!");
            return;
        }

        Instance = this;
    
        CreatePool();
        CreateCamera();
        CreateStageManager();
        CreateTimeManager();
        CreateVolume();
        CreateLight();
        CreateUI();
        CreateAudio();
    }

    private void Start() {
        AudioManager.Instance.PlayBGM(_masterSource, "BGM");
        UIManager.Instance.ShowPanel(ScreenType.MainMenu);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            UIManager.Instance.ShowPanel(PopupType.Setting);
        }
    }

    private void CreatePool(){
        PoolManager.Instance = new PoolManager(_poolingTrm);

        _poolingList.Pairs.ForEach(pair => {
            PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
        });
    }

    private void CreateStageManager(){
        StageManager.Instance = new StageManager();
    }

    private void CreateCamera(){
        CameraManager.Instance = GetComponent<CameraManager>();
    }

    private void CreateTimeManager(){
        TimeManager.Instance = new TimeManager();
    }

    private void CreateVolume(){
        VolumeManager.Instance = GetComponent<VolumeManager>();
    }

    private void CreateLight(){
        LightManager.Instance = GetComponent<LightManager>();
    }

    private void CreateUI(){
        UIManager.Instance = GetComponent<UIManager>();
    }

    private void CreateAudio(){
        AudioManager.Instance = GetComponent<AudioManager>();
    }

    public void StageEnd(){
        StartCoroutine(StageEndCoroutine());
    }

    private IEnumerator StageEndCoroutine(){
        yield return new WaitForSecondsRealtime(2f);
        StageManager.Instance.ExitStage();
        UIManager.Instance.ShowPanel(ScreenType.MainMenu);
    }

    public void RunCoroutine(IEnumerator routine){
        StartCoroutine(routine);
    }
}
