using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : PoolableMono
{
    [SerializeField]
    private Transform _startPos;

    [SerializeField]
    private Transform _enemyParent;

    [SerializeField]
    private List<Transform> _enemyPos = new List<Transform>();

    public void Setting(){
        _enemyParent.GetComponentsInChildren<Transform>(_enemyPos);
        _enemyPos.RemoveAt(0);

        PlayerController player = PoolManager.Instance.Pop("Player") as PlayerController;
        player.GetModule<PlayerMovementModule>().CharController.enabled = false;
        player.transform.position = _startPos.position;
        player.GetModule<PlayerMovementModule>().CharController.enabled = true;
        SpawnEnemy();
    }

    public void Release(){
        PoolManager.Instance.Push(this);
    }

    public void SpawnEnemy(){
        foreach(Transform pos in _enemyPos){
            Debug.Log(pos.position);
            EnemyController enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;
            enemy.GetModule<EnemyNavModule>().CharController.enabled = false;
            enemy.GetModule<EnemyNavModule>().NavMeshAgent.enabled = false;
            enemy.transform.position = pos.position;
            enemy.GetModule<EnemyNavModule>().CharController.enabled = true;
            enemy.GetModule<EnemyNavModule>().NavMeshAgent.enabled = true;
        }
    }

    public override void Init(){}
}
