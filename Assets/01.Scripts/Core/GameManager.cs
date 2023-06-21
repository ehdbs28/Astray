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
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)){
            StageManager.Instance.SetStage();
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

    public void RunCoroutine(IEnumerator routine){
        StartCoroutine(routine);
    }
}
