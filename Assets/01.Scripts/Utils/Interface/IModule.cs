using UnityEngine;

public interface IModule
{
    public void SetUp(Transform agentRoot);
    public void OnEnterModule();
    public void OnUpdateModule();
    public void OnFixedUpdateModule();
    public void OnDestroyModule();
}