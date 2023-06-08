using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonModule<T> : MonoBehaviour, IModule where T : ModuleController
{
    protected T _controller;

    public virtual void SetUp(Transform agentRoot)
    {
        _controller = agentRoot.GetComponent<T>();
    }

    private void OnDisable() {
        OnExitModule();
    }

    public abstract void OnEnterModule();
    public abstract void OnUpdateModule();
    public abstract void OnFixedUpdateModule();
    public abstract void OnExitModule();
}
