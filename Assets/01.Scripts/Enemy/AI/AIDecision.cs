using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    [SerializeField]
    private bool _isReverse;
    public bool IsReverse => _isReverse;

    protected ModuleController _controller;

    public virtual void SetUp(Transform root){
        _controller = root.GetComponent<ModuleController>();
    }

    public abstract bool MakeADecision();
}
