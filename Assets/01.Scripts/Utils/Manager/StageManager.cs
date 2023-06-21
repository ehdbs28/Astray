using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public static StageManager Instance;

    private int _currentStageNum = 1;
    public int CurrentStageNum { get => _currentStageNum; set => _currentStageNum = value; }

    private Stage _currentStage = null;

    public void SetStage(){
        _currentStage?.Release();
        _currentStage = PoolManager.Instance.Pop($"Stage{_currentStageNum}") as Stage;
        _currentStage?.Setting();
    }

    public void ExitStage(){
        if(_currentStage != null){
            PoolManager.Instance.Push(_currentStage);
            _currentStage = null;
        }
    }
}
