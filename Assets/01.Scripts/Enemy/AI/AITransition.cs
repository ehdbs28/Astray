using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [SerializeField]
    private AIModule _nextModule;
    public AIModule NextModule => _nextModule;

    private List<AIDecision> _decisions;

    public void SetUp(Transform root){
        _decisions = new List<AIDecision>();
        GetComponents<AIDecision>(_decisions);
        _decisions.ForEach(d => d.SetUp(root));
    }

    public bool CheckDecision(){
        bool result = false;

        foreach(var d in _decisions){
            result = d.MakeADecision();

            if(d.IsReverse){
                result = !result;
            }

            if(result == false){
                break;
            }
        }

        return result;
    }
}
