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
        CreateStageManager();
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
}
