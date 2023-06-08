using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    [SerializeField]
    private bool _isReverse;
    public bool IsReverse => _isReverse;

    protected EnemyController _controller;

    public virtual void SetUp(Transform root){
        _controller = root.GetComponent<EnemyController>();
    }

    public abstract bool MakeADecision();
}
