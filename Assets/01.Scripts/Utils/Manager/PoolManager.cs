using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance = null;

    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent){
        _trmParent = trmParent;
    }

    public void CreatePool(PoolableMono prefab, int count){
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _trmParent, count);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolableMono Pop(string prefabName){
        if(_pools.ContainsKey(prefabName) == false){
            Debug.LogError($"[POOLING] Prefab does not exist on pool : {prefabName}");
            return null;
        }

        PoolableMono obj = _pools[prefabName].Pop();
        obj.Init();
        return obj;
    }

    public void Push(PoolableMono obj){
        _pools[obj.gameObject.name].Push(obj);
    }
}
