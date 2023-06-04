using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolingPair{
    public PoolableMono Prefab;
    public int Count;
}

[CreateAssetMenu(menuName = "SO/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> Pairs;
}
