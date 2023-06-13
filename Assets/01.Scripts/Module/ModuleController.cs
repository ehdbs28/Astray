using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleController : PoolableMono
{
    protected readonly List<IModule> _modules = new List<IModule>();

    protected virtual void Awake(){
        transform.Find("Modules").GetComponentsInChildren<IModule>(_modules);
        foreach(var module in _modules){
            module.SetUp(transform);
        }
    }

    protected virtual void Start(){
        foreach(var module in _modules){
            module.OnEnterModule();
        }
    }

    protected virtual void Update(){
        foreach(var module in _modules){
            module.OnUpdateModule();
        }
    }

    protected virtual void FixedUpdate(){
        foreach(var module in _modules){
            module.OnFixedUpdateModule();
        }
    }

    public T GetModule<T>() where T : IModule{
        T returnModule = default(T);
        
        foreach(var module in _modules.OfType<T>()){
            returnModule = module;
        }

        return returnModule;
    }

    public override void Init(){}
}
